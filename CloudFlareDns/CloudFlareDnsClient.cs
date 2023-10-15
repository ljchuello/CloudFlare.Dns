using CloudFlareDns.Clients;

namespace CloudFlareDns
{
    public class CloudFlareDnsClient
    {
        public string XAuthKey { get; private set; }
        public string XAuthEmail { get; private set; }
        public string ZoneIdentifier { get; private set; }

        /// <summary>
        /// Start the constructor with the token, email, and the domain identifier
        /// </summary>
        /// <param name="xAuthKey">XAuthKey; It is the Global API Key, which can be found at http://dash.cloudflare.com/profile/api-tokens</param>
        /// <param name="xAuthEmail">XAuthEmail; It is the domain owner's email within Cloudflare</param>
        /// <param name="zoneIdentifier">ZoneIdentifier; It is the unique domain identifier, which can be found in the Overview section</param>
        public CloudFlareDnsClient(string xAuthKey, string xAuthEmail, string zoneIdentifier)
        {
            XAuthKey = xAuthKey;
            XAuthEmail = xAuthEmail;
            ZoneIdentifier = zoneIdentifier;

            Record = new RecordClient(XAuthKey, XAuthEmail, ZoneIdentifier);
        }

        public RecordClient Record { get; private set; }
    }
}
