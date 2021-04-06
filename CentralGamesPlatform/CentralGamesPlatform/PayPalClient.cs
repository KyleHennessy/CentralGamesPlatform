using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using PayoutsSdk.Core;
using PayPalHttp;

namespace CentralGamesPlatform
{
    public class PayPalClient
    {
        public static PayPalEnvironment environment()
        {
            return new SandboxEnvironment(
                 System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_ID") != null ?
                 System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_ID"): "AVESW8pAcvJQVcH6G11Z193gImhx8H8DQ9NIwV9sgP1XFZWaDztQym-JaolO1XX-gGsL69VLChgzePxb",
                 System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_SECRET") != null ?
                 System.Environment.GetEnvironmentVariable("PAYPAL_CLIENT_SECRET"): "ELslSeKzMAEFceAdQroyeo3gLrCU0f6G2b8zhQl54TpPPRfAXn1u5kfCT_MjyQHZHlRHc4D1cQFGNodt");
        }
        public static HttpClient client()
        {
            return new PayPalHttpClient(environment());
        }

        public static HttpClient client(string refreshToken)
        {
            return new PayPalHttpClient(environment(), refreshToken);
        }

        /**
            This method can be used to Serialize Object to JSON string.
        */
        public static String ObjectToJSONString(Object serializableObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            var writer = JsonReaderWriterFactory.CreateJsonWriter(
                        memoryStream, Encoding.UTF8, true, true, "  ");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(serializableObject.GetType(), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
            ser.WriteObject(writer, serializableObject);
            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);
            return sr.ReadToEnd();
        }
    }
}
