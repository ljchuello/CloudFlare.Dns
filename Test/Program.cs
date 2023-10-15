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
                CloudFlareDnsClient cloudFlareDnsClient = new CloudFlareDnsClient("", "", "");
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