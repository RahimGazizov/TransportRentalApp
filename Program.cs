using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;



namespace Подготовка
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = new TransportApp();
            app.Run();
        }
    }
    
}