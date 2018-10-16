using System;
using Newtonsoft.Json;

namespace NetworkEngine.Manager.Core.DataModels.Connector
{
    public class Node
    {
        [JsonProperty("uuid")]
        public Guid UUID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
