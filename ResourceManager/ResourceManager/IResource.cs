namespace ScottsJewels.Web.UI
{
    /// <summary>
    /// A Resource that can be managed by the Resource Manager.
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// The content type of the Resource.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// The path to the Resource.
        /// </summary>
        string Path { get; }
    }
}
