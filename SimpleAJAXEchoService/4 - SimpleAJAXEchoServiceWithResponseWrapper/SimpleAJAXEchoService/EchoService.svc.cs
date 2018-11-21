using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace SimpleAJAXEchoService
{
    /// <summary>
    /// The Echo Web Service.
    /// </summary>
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EchoService
    {
        /// <summary>
        /// Echos a Message.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <returns>The Echoed Message in JSON.</returns>
        /// <remarks>
        /// RequestFormat = WebMessageFormat.JSON converts the incoming parameter from a JSON string to an instance of Message.
        /// ResponseFormat = WebMessageFormat.JSON converts the result into a JSON string.
        /// </remarks>
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public ClientResponse<Message> Echo (Message message)
        {
            // Now wrapping up the response from the Echo Service to supply additional information to the client.

            ClientResponse<Message> result = new ClientResponse<Message>();

            if (!string.IsNullOrEmpty(message.Text))
            {
                result.Payload = new Message
                                     {
                                         Text = string.Format("Echo {0}", message.Text)
                                     };            
                result.IsSuccessful = true;
            }
            else
            {
                // Return an error if the client submitted a blank message.
                result.IsSuccessful = false;
                result.ErrorMessage = "Message text is empty!";
            }

            return result;            
        }        
    }
}
