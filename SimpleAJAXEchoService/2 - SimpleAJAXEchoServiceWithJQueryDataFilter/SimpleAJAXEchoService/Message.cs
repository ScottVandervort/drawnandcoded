using System.Runtime.Serialization;

namespace SimpleAJAXEchoService
{
    /// <summary>
    /// A Message that can be sent to the EchoService.
    /// </summary>
    [DataContract]
    public class Message
    {
        /// <summary>
        /// The content of the Message.
        /// </summary>
        [DataMember]
        public string Text { get; set; }
    }
}