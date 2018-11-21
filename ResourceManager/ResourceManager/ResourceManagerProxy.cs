using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ScottsJewels.Web.UI
{
    /// <summary>
    /// Only one instance of the Resource Manager can exist on a Page heirarchy. The Proxy has no such limitation and can be placed everywhere (if necessary). 
    /// The Proxy allows additional resources to be added to the Resource Manager.
    /// 
    /// A Proxy can only be used if a Resource Manager exists elsewhere in the Page's heriarchy.
    /// </summary>
    /// <typeparam name="TResource">The Resource type to manage.</typeparam> 
    /// <typeparam name="TResourceComparer">The Resource comparer. Used to prevent redunadant Resources references.</typeparam> 
    public class ResourceManagerProxy<TResource, TResourceComparer> : WebControl
        where TResourceComparer : EqualityComparer<TResource>, new()
        where TResource : IResource
    {
        #region Properties
        /// <summary>
        /// Managed resources.
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<TResource> Resources
        {
            get
            {
                if (ResourceManager<TResource, TResourceComparer>.Current != null)
                    return ResourceManager<TResource, TResourceComparer>.Current.Resources;

                throw new InvalidOperationException(
                    string.Format("No {0} ResourceManager has been declared.", typeof(TResource)));
            }
        }

        /// <summary>
        /// Managed resources that should be merged and added to the HTML DOM as a single element. Will be retrieved using a single HTTP GET Request.
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public CompositeResource<TResource> CompositeResource
        {
            get
            {
                if (ResourceManager<TResource, TResourceComparer>.Current != null)
                    return ResourceManager<TResource, TResourceComparer>.Current.CompositeResource;

                throw new InvalidOperationException(
                    string.Format("No {0} ResourceManager has been declared.", typeof(TResource)));
            }
        }

        /// <summary>
        /// Managed resources that should be deferred to the bottom of the HTML DOM. This will defer processing by a web client / browser until the Page has (visually) loaded.
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public DeferredResources<TResource> DeferredResources
        {
            get
            {
                if (ResourceManager<TResource, TResourceComparer>.Current != null)
                    return ResourceManager<TResource, TResourceComparer>.Current.DeferredResources;

                throw new InvalidOperationException(
                    string.Format("No {0} ResourceManager has been declared.", typeof(TResource)));
            }
        }
        #endregion
    }
}
