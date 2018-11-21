namespace ScottsJewels.Web.UI
{
    /// <summary>
    /// A Javascript (JS) Reference.
    /// </summary>
    public class Script : IResource
    {
        #region Properties
        /// <summary>
        /// The path to the script.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// The script content type. 
        /// </summary>
        public string ContentType
        {
            get
            {
                return "text/javascript";
            }
        }
        #endregion
    }

}
