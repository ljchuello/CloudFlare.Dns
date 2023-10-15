using System.Collections.Generic;
using CloudFlareDns.Objects.Universal;
using Newtonsoft.Json;

namespace CloudFlareDns.Objects.Record.Get
{
    public class Response
    {
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [JsonProperty("result")]
        public List<Record> Result { get; set; } = new List<Record>();

        [JsonProperty("result_info")]
        public ResultInfo ResultInfo { get; set; } = new ResultInfo();

        [JsonProperty("errors")]
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
