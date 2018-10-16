using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetworkEngine.Manager.Core.DataModels;
using NetworkEngine.Manager.Core.DataModels.Connector;
using NetworkEngine.Manager.Core.Exceptions;
using NetworkEngine.Manager.Core.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetworkEngine.Manager.Core.Connector
{
    public class Connector
    {
        private TcpClient _client = new TcpClient();
        private NetworkStream _stream;

        private byte[] _buffer;
        private byte[] _totalBuffer;

        private readonly ConcurrentDictionary<string, PacketState> _packetStates;

        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);

        public Connector()
        {
            _buffer = new byte[1024];
            _totalBuffer = new byte[0];

            _packetStates = new ConcurrentDictionary<string, PacketState>();

            // Pass through all tunneled packets
            Subscribe<TunneledPacket<dynamic>>("tunnel/send", json =>
            {
                OnPacket(json.Data.Data.ID, json.Data);
            });
        }

        public async Task Connect(string ip, int port)
        {
            await _client.ConnectAsync(ip, port);
            _stream = _client.GetStream();
            _stream.BeginRead(_buffer, 0, _buffer.Length, OnRead, null);
        }

        /// <summary>
        /// Subscribes to a specific packet id
        /// </summary>
        /// <typeparam name="T">The expected return type</typeparam>
        /// <param name="id">The packet id</param>
        /// <param name="callback">The callback</param>
        public void Subscribe<T>(string id, Action<Packet<T>> callback)
        {
            var packetState = _packetStates.GetOrAdd(id, new PacketState());

            packetState.Subscriptions.Add((json) => callback(json.ToObject<Packet<T>>()));
        }

        /// <summary>
        /// Sends a packet asynchronous to the Network Engine
        /// </summary>
        /// <typeparam name="T">The expected return type</typeparam>
        /// <param name="id">The packet id</param>
        /// <param name="data">The packet data</param>
        /// <returns></returns>
        public async Task<T> SendAsync<T>(string id, object data = null)
        {
            var packetState = _packetStates.GetOrAdd(id, new PacketState());

            await SendDataAsync(new
            {
                id,
                data
            });

            await packetState.OnComplete.WaitAsync();

            return packetState.Response.data.ToObject<T>();
        }

        /// <summary>
        /// Sends a tunneled pack asynchronous to the Network Engine
        /// </summary>
        /// <typeparam name="T">The expected return type</typeparam>
        /// <param name="destination">The destination tunnel to send to</param>
        /// <param name="id">The packet id</param>
        /// <param name="data">The packet data</param>
        /// <returns></returns>
        public async Task<T> SendTunneledAsync<T>(Tunnel destination, string id, object data = null)
        {
            if (destination == null)
            {
                throw new ArgumentException("The provided destination tunnel was null!", nameof(destination));
            }

            var packetState = _packetStates.GetOrAdd(id, new PacketState());

            await SendDataAsync(new
            {
                id = "destination/send",
                data = new
                {
                    dest = destination.ID,
                    data = new
                    {
                        id,
                        data
                    }
                }
            });

            await packetState.OnComplete.WaitAsync();

            return packetState.Response.ToObject<T>();
        }

        #region Internal Send / Read Methods

        private async Task SendDataAsync(object data)
        {
            // Wait for the lock to open, in case we where already sending
            await _sendLock.WaitAsync();
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));

                await _stream.WriteAsync(BitConverter.GetBytes(bytes.Length), 0, 4);
                await _stream.WriteAsync(bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                // We are done, so release the lock
                _sendLock.Release();
            }
        }

        private void OnRead(IAsyncResult ar)
        {
            int received = _stream.EndRead(ar);
            _totalBuffer = _totalBuffer.Concat(_buffer, received);

            if (_totalBuffer.Length >= 4)
            {
                int packetSize = BitConverter.ToInt32(_totalBuffer, 0);

                if (_totalBuffer.Length >= packetSize + 4)
                {
                    string data = Encoding.UTF8.GetString(_totalBuffer, 4, packetSize);
                    dynamic json = JsonConvert.DeserializeObject(data);

                    OnPacket(json.id.ToString(), json);

                    _totalBuffer = _totalBuffer.Skip(4 + packetSize).ToArray();
                }
            }

            _stream.BeginRead(_buffer, 0, _buffer.Length, OnRead, null);
        }

        private void OnPacket(string id, dynamic json)
        {
            if (_packetStates.ContainsKey(id))
            {
                var callbackEntry = _packetStates[id];

                callbackEntry.Response = json;

                lock (callbackEntry.Subscriptions)
                {
                    foreach (var subscription in callbackEntry.Subscriptions)
                    {
                        subscription?.Invoke(json);
                    }
                }

                // Something is waiting for us
                if (callbackEntry.OnComplete.CurrentCount == 0)
                {
                    callbackEntry.OnComplete.Release();
                }
            }
            else
            {
                Console.WriteLine($"[WARN]: Got an unhandled packet {id}");
            }
        }

        #endregion

        class PacketState
        {
            public readonly SemaphoreSlim OnComplete = new SemaphoreSlim(0, 1);
            public dynamic Response { get; set; }

            public List<Action<dynamic>> Subscriptions { get; }

            public PacketState()
            {
                Subscriptions = new List<Action<dynamic>>();
            }
        }

        public class Packet<T>
        {
            public string ID { get; set; }
            public T Data { get; set; }

            public override string ToString()
            {
                return $"{nameof(ID)}: {ID}, {nameof(Data)}: {Data}";
            }
        }

        class TunneledPacket<T>
        {
            [JsonProperty("id")]
            public Guid From { get; set; }
            public Packet<T> Data { get; set; }
        }
    }
}
