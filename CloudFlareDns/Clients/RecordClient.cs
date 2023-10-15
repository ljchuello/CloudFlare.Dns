using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudFlareDns.Objects.Record;
using CloudFlareDns.Objects.Record.Get;
using Newtonsoft.Json.Linq;

namespace CloudFlareDns.Clients
{
    public class RecordClient
    {
        private readonly string _xAuthKey;
        private readonly string _xAuthEmail;
        private readonly string _zoneIdentifier;

        public RecordClient(string xAuthKey, string xAuthEmail, string zoneIdentifier)
        {
            _xAuthKey = xAuthKey;
            _xAuthEmail = xAuthEmail;
            _zoneIdentifier = zoneIdentifier;
        }

        /// <summary>
        /// List DNS Records
        /// </summary>
        /// <returns></returns>
        public async Task<List<Record>> Get()
        {
            List<Record> listDns = new List<Record>();
            long page = 0;
            while (true)
            {
                // Nex
                page++;

                // Get list
                string json = await Core.SendGetRequest(_xAuthKey, _xAuthEmail, $"/zones/{_zoneIdentifier}/dns_records?page={page}&per_page={Core.PerPage}");
                Response response = JsonConvert.DeserializeObject<Response>(json) ?? new Response();

                // We iterate
                foreach (Record row in response.Result)
                {
                    listDns.Add(row);
                }

                // Finish?
                if (response.ResultInfo.Page == response.ResultInfo.TotalPages)
                {
                    // Yes, finish
                    return listDns;
                }
            }
        }

        /// <summary>
        /// DNS Record Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Record> Get(string id)
        {
            string json = await Core.SendGetRequest(_xAuthKey, _xAuthEmail, $"/zones/{_zoneIdentifier}/dns_records/{id}");
            JObject result = JObject.Parse(json);
            Record record = JsonConvert.DeserializeObject<Record>($"{result["result"]}") ?? new Record();
            return record;
        }

        /// <summary>
        /// Create DNS Record
        /// </summary>
        /// <param name="content">A valid IPv4/IPv6 address or url</param>
        /// <param name="name">DNS record name (or @ for the zone apex) in Punycode</param>
        /// <param name="proxied">Whether the record is receiving the performance and security benefits of Cloudflare</param>
        /// <param name="type">Record type</param>
        /// <param name="comment">Comments or notes about the DNS record. This field has no effect on DNS responses</param>
        /// <param name="ttl">Time To Live (TTL) of the DNS record in seconds. Setting to 1 means 'automatic'. Value must be between 60 and 86400, with the minimum reduced to 30 for Enterprise zones</param>
        /// <returns></returns>
        public async Task<Record> Create(string name, string content,bool proxied, RecordType type, int ttl, string comment = "")
        {
            // To json
            string raw = $"{{ \"content\": \"{content}\", \"name\": \"{name}\", \"proxied\": {(proxied ? "true" : "false")}, \"type\": \"{type}\", \"comment\": \"{comment}\", \"ttl\": {ttl} }}";

            // Send
            string json = await Core.SendPostRequest(_xAuthKey, _xAuthEmail, $"/zones/{_zoneIdentifier}/dns_records/", raw);

            // Set
            JObject result = JObject.Parse(json);
            Record record = JsonConvert.DeserializeObject<Record>($"{result["result"]}");

            // Free
            return record;
        }

        /// <summary>
        /// Update DNS Record
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task<Record> Update(Record record)
        {
            // To json
            string raw = $"{{ \"content\": \"{record.Content}\", \"name\": \"{record.Name}\", \"proxied\": {(record.Proxied ? "true" : "false")}, \"type\": \"{record.Type}\", \"comment\": \"{record.Comment}\", \"ttl\": {record.Ttl} }}";

            // Send
            string json = await Core.SendPutRequest(_xAuthKey, _xAuthEmail, $"/zones/{_zoneIdentifier}/dns_records/{record.Id}", raw);

            // Set
            JObject result = JObject.Parse(json);
            record = JsonConvert.DeserializeObject<Record>($"{result["result"]}");

            // Free
            return record;
        }

        /// <summary>
        /// Patch DNS Record
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task<Record> Patch(Record record)
        {
            // To json
            string raw = $"{{ \"content\": \"{record.Content}\", \"name\": \"{record.Name}\", \"proxied\": {(record.Proxied ? "true" : "false")}, \"type\": \"{record.Type}\", \"comment\": \"{record.Comment}\", \"ttl\": {record.Ttl} }}";

            // Send
            string json = await Core.SendPatchRequest(_xAuthKey, _xAuthEmail, $"/zones/{_zoneIdentifier}/dns_records/{record.Id}", raw);

            // Set
            JObject result = JObject.Parse(json);
            record = JsonConvert.DeserializeObject<Record>($"{result["result"]}") ?? new Record();

            // Free
            return record;
        }

        /// <summary>
        /// Delete DNS Record
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task Delete(Record record)
        {
            await Delete(record.Id);
        }

        /// <summary>
        /// Delete DNS Record
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public async Task Delete(string recordId)
        {
            await Core.SendDeleteRequest(_xAuthKey, _xAuthEmail, $"/zones/{_zoneIdentifier}/dns_records/{recordId}");
        }
    }
}
