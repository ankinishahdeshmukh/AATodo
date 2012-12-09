using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using AATodo;


namespace AATodoHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:9876/");

            //WebServiceHost svcHost = new WebServiceHost(typeof(AATodo.CalcService), baseAddress);
            WebServiceHost svcHost = new WebServiceHost(new AATodoService(), baseAddress);

            try
            {
                svcHost.Open();

                Console.WriteLine("Service is running at " + baseAddress);
                Console.WriteLine("Press enter to quit...");
                Console.ReadLine();

                svcHost.Close();
            }
            catch (CommunicationException cex)
            {
                Console.WriteLine("An exception occurred: {0}", cex.Message);
                svcHost.Abort();
            }
        }
    }
}
