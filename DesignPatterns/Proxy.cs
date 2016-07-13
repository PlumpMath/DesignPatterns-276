using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public interface IClient
    {
        string GetMessage();
    }
   
    public class ClientProxy : IClient
    {
        private class Client : IClient
        {
            public string GetMessage()
            {
                return "Hello World";
            }
        }

        private IClient ClientInstance { get; set; }

        public int Authenticate(string userName, string password)
        {
            if (userName == "vishal" && password == "pass@123")
            {
                return 1;
            }

            return -1;
        }

        public IClient GetClientInstance()
        {
            if (ClientInstance == null)
            {
                Console.WriteLine("User name : ");
                var userName = Console.ReadLine();
                Console.WriteLine("Password : ");
                var password = Console.ReadLine();

                var authenticateResult = Authenticate(userName, password);
                if (authenticateResult == 1)
                {
                    ClientInstance = new Client();
                    return ClientInstance;
                }

                throw new UnauthorizedAccessException($"Authentication failed for user { userName } ");
            }

            return ClientInstance;
        }

        public string GetMessage()
        {
            if (ClientInstance == null)
            {
                return "Authenticate user first";
            }

            return ClientInstance.GetMessage();
        }
    } 


    class Proxy
    {
        static void Main(string[] args)
        {
            ClientProxy proxy = new ClientProxy();
            var message  = proxy.GetMessage();
            Console.WriteLine(message);
           
            var client = proxy.GetClientInstance();

            if (client != null)
            {
                message = client.GetMessage();
                Console.WriteLine(message);

                message = client.GetMessage();
                Console.WriteLine(message);
            }
        }
    }
}
