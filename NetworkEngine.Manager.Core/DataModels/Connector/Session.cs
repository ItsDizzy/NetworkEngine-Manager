using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetworkEngine.Manager.Core.DataModels.Connector
{
    public class Session
    {
        [JsonProperty("id")]
        public Guid ID { get; set; }

        [JsonProperty("clientinfo")]
        public ClientInfo ClientInfo { get; set; }

        [JsonProperty("startTime")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime StartTime { get; set; }

        [JsonProperty("lastPing")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastPing { get; set; }

        [JsonProperty("features")]
        public List<string> Features { get; set; }

        public override string ToString()
        {
            return $@"Session
{{
    {nameof(ID)}: {ID},
    {nameof(ClientInfo)}: {ClientInfo},
    {nameof(StartTime)}: {StartTime},
    {nameof(LastPing)}: {LastPing},
    {nameof(Features)}: {string.Join(", ", Features)}
}}";
        }
    }

    public class ClientInfo
    {
        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("renderer")]
        public string Renderer { get; set; }

        [JsonProperty("file")]
        public string FileName { get; set; }

        public override string ToString()
        {
            return $@"ClientInfo
{{
    {nameof(Host)}: {Host},
    {nameof(User)}: {User},
    {nameof(Renderer)}: {Renderer},
    {nameof(FileName)}: {FileName}
}}";
        }
    }
}
