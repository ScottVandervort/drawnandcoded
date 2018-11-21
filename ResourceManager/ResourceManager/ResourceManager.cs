using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ScottsJewels.Web.UI
{
    /// <summary>
    /// This is the base class for all Resource Managers. 
    /// 
    /// The Resource Manager is responsible for managing a collection of resources and injecting the necessary HTML 
    /// elements (i.e. <script>, <link>, etc..) into the Page so that they can be retrieved by the web browser / client. 
    /// 
    /// The Resource Manager is not responsible for serving the actual resource content. Resources are served by the HttpResourceHandler.
    /// 
    /// Only one instance of the Resource Manager can exist per Page heirarchy. This is done to consolidate resource management and rendering to a single entity.
    /// If resources needed to be added and a reference to the Resource Manager is unavailable the ResourceManagerProxy can use used instead.
    /// </summary>
    /// <typeparam name="TResource">The Resource handled by the HTTP Handler.</typeparam>
    /// <typeparam name="TResourceComparer">The EqualityComparer class to use when comparing Resources.</typeparam>
    public abstract class ResourceManager<TResource, TResourceComparer> : WebControl
        where TResourceComparer : EqualityComparer<TResource>, new()
        where TResource : IResource
    {
        #region Data Members
        /// <summary>
        /// A placeholder control injected into the Page that allows resources to be rendered in the HTML HEAD.
        /// </summary>
        private readonly LiteralControl _headerPlaceholder;
        /// <summary>
        /// A placeholder control injected into the Page that allows resources to be rendered in the HTML footer.
        /// </summary>
        private readonly LiteralControl _footerPlaceholder;
        private readonly CompositeResource<TResource> _compositeResource;
        private readonly Resources<TResource> _resources;
        private readonly DeferredResources<TResource> _deferredResource;
        /// <summary>
        /// A unique identifier for the ResourceManager that allows it to be treated as a singleton.
        /// </summary>
        private static readonly string CachIdentifier;
        /// <summary>
        /// An instance of the TResource equality comparer. Used to eliminate redundant Resources.
        /// </summary>
        private static readonly TResourceComparer Comparer = new TResourceComparer();
        #endregion

        #region Properties
        /// <summary>
        /// Managed resources.
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Resources<TResource> Resources
        {
            get
            {
                return _resources;
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
                return _compositeResource;
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
                return _deferredResource;
            }
        }

        /// <summary>
        /// Resources that will be added to the HTML DOM - each with a unique element. Each Resource results in a unique HTTP GET Request.
        /// </summary>
        internal static ResourceManager<TResource, TResourceComparer> Current
        {
            get
            {
                ResourceManager<TResource, TResourceComparer> result = null;

                if (((HttpContext.Current != null) && (HttpContext.Current.Items[CachIdentifier] != null)) &&
                    (HttpContext.Current.Items[CachIdentifier] is ResourceManager<TResource, TResourceComparer>))
                {
                    result = HttpContext.Current.Items[CachIdentifier] as ResourceManager<TResource, TResourceComparer>;
                }

                return result;
            }
        }


        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        static ResourceManager()
        {
            CachIdentifier = typeof(ResourceManager<TResource, TResourceComparer>).ToString();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ResourceManager()
        {
            _resources = new Resources<TResource>();
            _compositeResource = new CompositeResource<TResource>();
            _deferredResource = new DeferredResources<TResource>();
            _headerPlaceholder = new LiteralControl();
            _footerPlaceholder = new LiteralControl();

            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(CachIdentifier))
                {
                    throw new InvalidOperationException(string.Format("Only one {0} is allowed per page!", GetType()));
                }
                HttpContext.Current.Items[CachIdentifier] = this;
            }
        }
        #endregion

        #region Methods

        protected override void OnInit(EventArgs e)
        {
            // ASP.NET restricts you from modifying a Page's control collection during a WebControl's  OnInit, OnLoad, OnPreRender, OnRender, etc... events.
            // A Page's events do not have such restrictions. As such we will attach a handler to the Page'S PreRender event and inject our logic there.
            Page.PreRender += OnPagePreRender;
        }

        private void OnPagePreRender(object sender, EventArgs e)
        {
            // Make sure the HTML <head> is being "picked up" by ASP.NET. We'll need it to inject our Resources.
            if (Page.Header == null)
            {
                throw new Exception(
                    string.Format(
                        "{0} requires that a <head> element exists and that it has the runat=server attribute!",
                        (GetType())));
            }

            // Inject some placeholder controls into HTML <head> and footer. The Resource Manager render to these placeholders rather than it's own 
            // location in the markup. 
            Page.Header.Controls.Add(_headerPlaceholder);
            Page.Controls.Add(_footerPlaceholder);
        }

        protected override void OnPreRender(EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (StringWriter stringWriter = new StringWriter(stringBuilder))
            {
                using (HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter))
                {
                    // Allow the Resource Manager to render the Resources.
                    if ((_resources.Count > 0) || (_compositeResource.Resources.Count > 0))
                    {
                        RenderResources(htmlTextWriter, _resources.Distinct(Comparer).ToList(),
                                        _compositeResource.Resources.Distinct(Comparer).ToList());
                    }

                    _headerPlaceholder.Text = stringBuilder.ToString();
                    stringBuilder.Clear();

                    // Allow the Resource Manager to render the Deferred Resources.
                    if ((_deferredResource.Resources.Count > 0) || (_deferredResource.CompositeResource.Resources.Count > 0))
                    {
                        RenderResources(htmlTextWriter, _deferredResource.Resources.Distinct(Comparer).ToList(),
                                        _deferredResource.CompositeResource.Resources.Distinct(Comparer).ToList());
                    }

                    _footerPlaceholder.Text = stringBuilder.ToString();
                }
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// Renders the resources managed by this control.
        /// </summary>
        /// <param name="output">A HtmlTextWriter that represents the output stream to render HTML content on the client.</param>
        /// <param name="resources">The non-composite resources to render.</param>
        /// <param name="compositeResources">The composite resources to render.</param>
        protected abstract void RenderResources(HtmlTextWriter output, List<TResource> resources, List<TResource> compositeResources);

        public override void Dispose()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items.Remove(CachIdentifier);
            }
            base.Dispose();
        }

        #endregion
    }
}
