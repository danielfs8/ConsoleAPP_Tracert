using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Net;
using System;
using System.Net.Sockets;
using ConsoleApp_Tracert.Model;


//Referencia Estudar: https://docs.microsoft.com/en-us/dotnet/api/system.net.networkinformation.pingcompletedeventargs?redirectedfrom=MSDN&view=netframework-4.8
//Referencia: https://stackoverflow.com/questions/142614/traceroute-and-ping-in-c-sharp
namespace ConsoleApp_Trace
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Write("IP ou Host: ");
            Tracert.GetTraceRoute(Console.ReadLine());
        }




        



    }
}
