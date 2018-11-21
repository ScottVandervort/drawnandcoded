using System.Runtime.Serialization;

namespace SimpleAJAXEchoService
{
    /// <summary>
    /// A generic client response wrapper. Wraps a web service response so that 
    /// additional (troubleshooting) information can be returned alongside the payload.
    /// </summary>
    /// <typeparam name="T">The payload type to encapsulate.</typeparam>
    [DataContract]
    public class ClientResponse<T>
    {
        /// <summary>
        /// The data to return to the client.
        /// </summary>
        [DataMember]
        public T Payload { get; set; }

        /// <summary>
        /// True, if the data was retrieved sucessfully.
        /// </summary>
        [DataMember]
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// The error message if the data was not retrieved sucessfully.
        /// </summary>
        [DataMember]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ClientResponse()
        {
            IsSuccessful = true;
            ErrorMessage = string.Empty;            
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isSuccessful">True, if the data was retrieved sucessfully.</param>
        /// <param name="payload">The data to return to the client.</param>
        /// <param name="errorMessage">The error message if the data was not retrieved sucessfully.</param>
        public ClientResponse(bool isSuccessful, T payload, string errorMessage) : this()
        {
            IsSuccessful = isSuccessful;
            Payload = payload;
            ErrorMessage = errorMessage;
        }
    }
}
