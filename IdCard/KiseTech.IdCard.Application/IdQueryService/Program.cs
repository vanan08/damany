using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace IdQueryService
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(IdQueryProvider));
            host.Open();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
