namespace SimpleAJAXEchoService
{
    /// <summary>
    /// An Web Page that "talks" to the Echo Service using AJAX.
    /// </summary>
    public partial class EchoPage : System.Web.UI.Page
    {        
        // The addition of the .GetJsonString() method eliminates all of this code. Furthermore, it can be called against any class derived from 
        // object (which is pretty much anything).
        
        ///// <summary>
        ///// A serialized JSON representation of the Message class. Will be exposed to the EchoPage's Javascript and used to submit data to 
        ///// the EchoService.
        ///// </summary>
        //protected string jsonMessage;

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    // We'll need to submit an instance of the Message class to the EchoService from the EchoPage using Javascript. JSON is an ideal format to 
        //    // work with. Let's serialize an instance of Message into a JSON string and expose it to the EchoPage's Javascript.
        //    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Message));
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        serializer.WriteObject(ms, new Message());
        //        jsonMessage = Encoding.Default.GetString(ms.ToArray()); // Viola! 
        //    }

        //}

    }
}
