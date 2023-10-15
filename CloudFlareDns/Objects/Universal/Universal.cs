using Newtonsoft.Json;

namespace CloudFlareDns.Objects.Universal
{
    public class Meta
    {
        [JsonProperty("auto_added")]
        public bool AutoAdded { get; set; } = false;

        [JsonProperty("managed_by_apps")]
        public bool ManagedByApps { get; set; } = false;

        [JsonProperty("managed_by_argo_tunnel")]
        public bool ManagedByArgoTunnel { get; set; } = false;

        [JsonProperty("source")]
        public string Source { get; set; } = string.Empty;
    }

    public class ResultInfo
    {
        [JsonProperty("page")]
        public int Page { get; set; } = 0;

        [JsonProperty("per_page")]
        public int PerPage { get; set; } = 0;

        [JsonProperty("count")]
        public int Count { get; set; } = 0;

        [JsonProperty("total_count")]
        public int TotalCount { get; set; } = 0;

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; } = 0;
    }

    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; } = 0;

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}
