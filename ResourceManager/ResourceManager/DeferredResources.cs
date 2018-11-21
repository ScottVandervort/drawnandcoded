using System.Web.UI;

namespace ScottsJewels.Web.UI
{
    /// <summary>
    /// Deferred Resources will be injected at the end of a Page. 
    /// Deferring a Resource results in a (visually) quicker load time by a web browser as the Resource won't be retrieved until after the Page has loaded. 
    /// Deferred Resources can contain a Composite Resource. 
    /// </summary>
    /// <typeparam name="TResource">The Resource type.</typeparam>    
    [ParseChildren(true)]
    public class DeferredResources<TResource>
        where TResource : IResource
    {
        #region Data Members

        readonly private Resources<TResource> _resources = new Resources<TResource>();
        readonly private CompositeResource<TResource> _compositeResource = new CompositeResource<TResource>();
        #endregion

        #region Properties
        public Resources<TResource> Resources
        {
            get
            {
                return _resources;
            }
        }
        public CompositeResource<TResource> CompositeResource
        {
            get
            {
                return _compositeResource;
            }
        }

        #endregion
    }
}
