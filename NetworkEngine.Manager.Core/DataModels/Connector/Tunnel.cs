using System;
using Newtonsoft.Json;

namespace NetworkEngine.Manager.Core.DataModels.Connector
{
    public class Tunnel
    {
        [JsonProperty("id")]
        public Guid ID { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public override string ToString()
        {
            return $@"Tunnel
{{
    {nameof(ID)}: {ID},
    {nameof(Status)}: {Status}
}}";
        }
    }
}
