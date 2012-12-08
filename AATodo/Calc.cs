using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace AATodo
{
    [ServiceContract]
    public interface ICalculator
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "add?x={x}&y={y}")]
        long Add(long x, long y);

        [OperationContract]
        [WebInvoke(UriTemplate = "sub?x={x}&y={y}")]
        long Subtract(long x, long y);

        [OperationContract]
        [WebInvoke(UriTemplate = "mult?x={x}&y={y}")]
        long Multiply(long x, long y);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Div?x={x}&y={y}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        long Divide(long x, long y);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Hello/{name}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SayHello(string name);
    }

    public class CalcService : ICalculator
    {
        public long Add(long x, long y)
        {
            return x + y;
        }

        public long Subtract(long x, long y)
        {
            return x - y;
        }

        public long Multiply(long x, long y)
        {
            return x * y;
        }

        public long Divide(long x, long y)
        {
            return x / y;
        }

        public string SayHello(string name)
        {
            return "Hello " + name;
        }
    }
}
