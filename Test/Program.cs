using CloudFlareDns;
using Newtonsoft.Json;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            try
            {
                // Variables
                string xAuthKey = ""; // Global API Key
                string xAuthEmail = ""; // Domain owner email in cloudflare
                string zoneIdentifier = ""; // Domain identifier

                // Client
                CloudFlareDnsClient cloudFlareDnsClient = new CloudFlareDnsClient(xAuthKey, xAuthEmail, zoneIdentifier);
            }
            catch (Exception ex)
            {
                string err = JsonConvert.SerializeObject(ex, Formatting.Indented);
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("Finish!.");
                Console.ReadLine();
            }
        }
    }
}