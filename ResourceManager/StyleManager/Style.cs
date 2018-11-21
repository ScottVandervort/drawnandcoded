namespace ScottsJewels.Web.UI
{
    /// <summary>
    /// A Stylesheet (CSS) Reference.
    /// </summary>
    public class Style : IResource
    {
        #region Properties
        /// <summary>
        /// The path to the stylesheet.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// The stylesheet media type (screen, print, etc...)
        /// </summary>
        public string Media { get; set; }
        /// <summary>
        /// The stylesheet content type. 
        /// </summary>
        public string ContentType
        {
            get
            {
                return "text/css";
            }
        }
        #endregion
    }

}
