using System;
using CloudFlareDns.Objects.Universal;
using Newtonsoft.Json;

namespace CloudFlareDns.Objects.Record
{
    public class Record
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Identifier
        /// </summary>
        [JsonProperty("zone_id")]
        public string ZoneId { get; set; } = string.Empty;

        /// <summary>
        /// The domain of the record
        /// </summary>
        [JsonProperty("zone_name")]
        public string ZoneName { get; set; } = string.Empty;

        /// <summary>
        /// DNS record name (or @ for the zone apex) in Punycode
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Record type
        /// </summary>
        [JsonProperty("type")]
        public RecordType Type { get; set; }

        /// <summary>
        /// A valid IPv4/IPv6/CNAME address
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Whether the record can be proxied by Cloudflare or not
        /// </summary>
        [JsonProperty("proxiable")]
        public bool Proxiable { get; set; } = false;

        /// <summary>
        /// Whether the record is receiving the performance and security benefits of Cloudflare
        /// </summary>
        [JsonProperty("proxied")]
        public bool Proxied { get; set; } = false;

        /// <summary>
        /// Time To Live (TTL) of the DNS record in seconds. Setting to 1 means 'automatic'. Value must be between 60 and 86400, with the minimum reduced to 30 for Enterprise zones
        /// </summary>
        [JsonProperty("ttl")]
        public int Ttl { get; set; } = 0;

        /// <summary>
        /// Whether this record can be modified/deleted (true means it's managed by Cloudflare)
        /// </summary>
        [JsonProperty("locked")]
        public bool Locked { get; set; } = false;

        /// <summary>
        /// Extra Cloudflare-specific information about the record
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; } = new Meta();

        /// <summary>
        /// Comments or notes about the DNS record. This field has no effect on DNS responses
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; } = string.Empty;

        //[JsonProperty("tags")]
        //public List<object> Tags { get; set; }

        /// <summary>
        /// When the record was created
        /// </summary>
        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; } = new DateTime(1900, 01, 01);

        /// <summary>
        /// When the record was last modified
        /// </summary>
        [JsonProperty("modified_on")]
        public DateTime ModifiedOn { get; set; } = new DateTime(1900, 01, 01);
    }

    public enum RecordType
    {
        A,
        AAAA,
        CAA,
        CERT,
        CNAME,
        DNSKEY,
        DS,
        HTTPS,
        LOC,
        MX,
        NAPTR,
        NS,
        PTR,
        SMIMEA,
        SRV,
        SSHFP,
        SVCB,
        TLSA,
        TXT,
        URI,
    }
}
