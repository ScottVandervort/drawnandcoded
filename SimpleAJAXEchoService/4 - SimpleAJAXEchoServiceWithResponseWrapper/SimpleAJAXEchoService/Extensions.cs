using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SimpleAJAXEchoService
{
    /// <summary>
    /// Extension methods to .NET classes.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Extends object to with a JSON serializer.
        /// </summary>        
        /// <returns>The object as a serialized JSON string.</returns>
        public static string GetJsonString(this object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }
    }
}
