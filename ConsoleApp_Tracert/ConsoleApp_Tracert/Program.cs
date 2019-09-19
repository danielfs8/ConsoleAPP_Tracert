using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Net;
using System;
using System.Net.Sockets;


//Referencia Estudar: https://docs.microsoft.com/en-us/dotnet/api/system.net.networkinformation.pingcompletedeventargs?redirectedfrom=MSDN&view=netframework-4.8
//Referencia: https://stackoverflow.com/questions/142614/traceroute-and-ping-in-c-sharp
namespace ConsoleApp_Trace
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Write("IP ou Host: ");
            GetTraceRoute(Console.ReadLine());
           
        }




        private const string Data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        public static IEnumerable<IPAddress> GetTraceRoute(string hostNameOrAddress)
        {
            Console.WriteLine(":::Inicializando análise de Rota:::\n");
            return GetTraceRoute(hostNameOrAddress, 1);
        }
        private static IEnumerable<IPAddress> GetTraceRoute(string hostNameOrAddress, int ttl)
        {
            int i = 0;
            Ping pinger = new Ping();
            PingOptions pingerOptions = new PingOptions(ttl, true);
            int timeout = 10000;
            byte[] buffer = Encoding.ASCII.GetBytes(Data);
            PingReply reply = default(PingReply);

            reply = pinger.Send(hostNameOrAddress, timeout, buffer, pingerOptions);

            List<IPAddress> result = new List<IPAddress>();
            if (reply.Status == IPStatus.Success)
            {
                result.Add(reply.Address);
            }
            else if (reply.Status == IPStatus.TtlExpired || reply.Status == IPStatus.TimedOut)
            {
                // adicione o endereço retornado no momento se um endereço foi encontrado com este TTL
                if (reply.Status == IPStatus.TtlExpired) result.Add(reply.Address);
                // recursão para obter o próximo endereço ...
                IEnumerable<IPAddress> tempResult = default(IEnumerable<IPAddress>);
                tempResult = GetTraceRoute(hostNameOrAddress, ttl + 1);
                result.AddRange(tempResult);
            }
            else
            {
                //Falhou 
            }


            //Pega o nome do Host
            //Referencia: https://stackoverflow.com/questions/11123639/how-to-resolve-hostname-from-local-ip-in-c-net
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(result[i]);
                if (entry.HostName != null)
                {
                    Console.WriteLine("Salto: " + ttl + " IP: " + result[i] + " Host: " + entry.HostName);
                }


            }
            catch (SocketException ex)
            {
                Console.WriteLine("Salto: " + ttl + " IP: " + result[i]);
            }



            return result;




        }



    }
}
