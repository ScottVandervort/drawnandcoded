using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ScottsJewels
{
    /// <summary>
    /// Extensions to the .NET Framework.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Converts an object into an JSON string.
        /// </summary>        
        /// <returns>An JSON string representing the object.</returns>
        /// <remarks>
        /// For more information check out http://scottsjewels.blogspot.com/2011/02/h1-font-size-28px-h2-text-transform.html
        /// </remarks>
        public static string ToJson(this object obj)
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
