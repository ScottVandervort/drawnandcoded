using System.Collections.Generic;

namespace ScottsJewels.Web.UI
{
    /// <summary>
    /// A collection of Resources.
    /// </summary>
    /// <typeparam name="TResource"></typeparam>
    public class Resources<TResource> : List<TResource>
        where TResource : IResource
    { }
}
