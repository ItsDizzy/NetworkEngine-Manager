using Newtonsoft.Json;

namespace NetworkEngine.Manager.Core.DataModels.Connector
{
    public class StatusResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        public override string ToString()
        {
            return $@"Status
{{
    {nameof(Status)}: {Status},
    {nameof(Error)}: {Error}
}}";
        }
    }
}
