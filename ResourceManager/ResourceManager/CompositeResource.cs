using System.Web.UI;

namespace ScottsJewels.Web.UI
{
    /// <summary>    
    /// A Composite Resource is a set of Resources that will be handled as a single bundled Resource. A single HTTP GET Request is used for the entire Composite Resource.
    /// </summary>
    /// <typeparam name="TResource">The Resource type.</typeparam>     
    [ParseChildren(true)]
    public class CompositeResource<TResource>
        where TResource : IResource
    {
        #region Data Members
        readonly private Resources<TResource> _resources = new Resources<TResource>();
        #endregion

        #region Properties
        /// <summary>
        /// The Resources to bundle in the Composite Resource.
        /// </summary>
        public Resources<TResource> Resources
        {
            get
            {
                return _resources;
            }
        }
        #endregion
    }
}
