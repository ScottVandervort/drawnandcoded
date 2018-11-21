using System;

namespace ScottsJewels.Web.UI
{
    public partial class UserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            styleManagerProxy.CompositeResource.Resources.Add(new Style() { Media = "screen", Path = "~/styles/UserControl1.css" });
            styleManagerProxy.CompositeResource.Resources.Add(new Style() { Media = "screen", Path = "~/styles/UserControl2.css" });
        }
    }
}