using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace apis.daemon
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var app = ConfidentialClientApplicationBuilder
                .Create("cdf60750-5e20-424a-a3c6-9b83a3aeceff")
                .WithClientSecret("cTF7Q~yHPaVLFH4GB_lYOJLl6Y-IrevvSl5cv")
                .WithAuthority("https://login.microsoftonline.com/eb2e1dfc-4f08-476f-b63c-f6302aaed2c2")
                .Build();

            AuthenticationResult result = null;
            try
            {
                var scopes = new string[] {"api://service1/.default"};
                result = await app.AcquireTokenForClient(scopes).ExecuteAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Token acquired \n");
                Console.WriteLine($"Bearer {result.AccessToken} \n");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Scope provided is not supported");
                Console.ResetColor();
            }

            Console.ReadKey();
        }
    }
}