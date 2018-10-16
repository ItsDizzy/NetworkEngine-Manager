using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetworkEngine.Manager.Core.DataModels.Connector;
using NetworkEngine.Manager.Core.Exceptions;
using NetworkEngine.Manager.Core.Services;
using Newtonsoft.Json.Linq;

namespace NetworkEngine.Manager.Core.Connector
{
    public class ConnectorService : IConnectorService
    {
        #region Public Properties

        /// <inheritdoc />
        public Connector Connector { get; }

        public Tunnel CurrentTunnel { get; private set; }

        #endregion

        #region Construction

        public ConnectorService()
        {
            Connector = new Connector();
        }

        /// <inheritdoc />
        public async Task Init()
        {
            await Connector.Connect("145.48.6.10", 6666);
        }

        #endregion

        #region Session / Tunnel Methods

        /// <inheritdoc />
        public async Task<List<Session>> GetSessions()
        {
            return await Connector.SendAsync<List<Session>>("session/list");
        }

        /// <inheritdoc />
        public async Task<Tunnel> OpenTunnel(Session session, string key = "")
        {
            if (!session.Features.Contains("tunnel"))
            {
                throw new TunnelException("Session does not support tunneling");
            }

            Console.WriteLine(session);

            var tunnel = await Connector.SendAsync<Tunnel>("tunnel/create", new
            {
                session = session.ID,
                key
            });

            Console.WriteLine(tunnel);

            if(tunnel.Status != "ok")
                throw new TunnelException("An error occured while connecting to given session, does it need a key perhaps?");

            return CurrentTunnel = tunnel;
        }

        #endregion

        #region Scene Methods

        /// <inheritdoc />
        public async Task<List<Node>> GetScene()
        {
            return await Connector.SendTunneledAsync<List<Node>>(CurrentTunnel, "scene/get");
        }

        /// <inheritdoc />
        public async Task<StatusResponse> ResetScene()
        {
            return await Connector.SendTunneledAsync<StatusResponse>(CurrentTunnel, "scene/reset");
        }

        #endregion

        #region Scene Node Methods

        /// <inheritdoc />
        public async Task<Node> AddSceneNode(string name, Node parent = null, object components = null)
        {
            return await Connector.SendTunneledAsync<Node>(CurrentTunnel, "scene/node/add", new
            {
                name,
                parent = parent?.UUID,
                components
            });
        }

        /// <inheritdoc />
        public async Task<StatusResponse> UpdateSceneNode(Node node, object data)
        {
            JObject obj = JObject.FromObject(new
            {
                id = node.UUID,
            });

            obj.Merge(JObject.FromObject(data), new JsonMergeSettings()
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });

            return await Connector.SendTunneledAsync<StatusResponse>(CurrentTunnel, "scene/node/update", obj);
        }

        /// <inheritdoc />
        public async Task<StatusResponse> DeleteSceneNode(Node node)
        {
            return await Connector.SendTunneledAsync<StatusResponse>(CurrentTunnel, "scene/node/delete", new
            {
                id = node.UUID
            });
        }

        /// <inheritdoc />
        public async Task<List<Node>> FindSceneNode(string name)
        {
            return await Connector.SendTunneledAsync<List<Node>>(CurrentTunnel, "scene/node/find", new
            {
                name
            });
        }

        #endregion
    }
}
