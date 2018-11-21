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
        public Message Echo (Message message)
        {
            message.Text = string.Format("Echo {0}", message.Text);

            return message;
        }        
    }
}
