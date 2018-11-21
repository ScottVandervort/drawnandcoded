using System;
using System.IO;
using System.Web;
using ScottsJewels.Web.UI;

namespace ScottsJewels.Web
{
    /// <summary>
    /// Handles HTTP Requests for Resources managed by the Resource Manager.
    /// 
    /// The HTML elements injected into an HTML page by a Resource Manager refer to an ResourceHttpHandler. It is the ResourceHttpHandler's job to serve these resources
    /// to the web browser / client.
    /// </summary>
    /// <typeparam name="TResource">The Resource handled by the HTTP Handler.</typeparam>
    public class ResourceHttpHandler<TResource> : IHttpHandler
        where TResource : IResource, new()
    {
        #region Data Members
        /// <summary>
        /// The HTTP Content Type is stored within the Resource being handled. This is a "dummy" Resource to reference when processing HTTP Requests.
        /// </summary>
        readonly static private TResource Resource = new TResource();
        #endregion

        #region Properties
        /// <summary>
        /// True, if the HTTP Handler should be reusable by other requests.
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Processes the specified HTTP Request.
        /// </summary>
        /// <param name="context">The HTTP Context.</param>
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["Resource"] != null)
            {
                string[] requestedResources = context.Request.QueryString["Resource"].Split(new[] { ',' }, Int32.MaxValue,
                                                                                 StringSplitOptions.RemoveEmptyEntries);
                string resourcePhysicalPath;

                foreach (string resource in requestedResources)
                {
                    resourcePhysicalPath = context.Server.MapPath(resource);

                    if (File.Exists(resourcePhysicalPath))
                    {
                        using (StreamReader reader = new StreamReader(resourcePhysicalPath))
                        {
                            context.Response.BinaryWrite(StringToByteArray(reader.ReadToEnd()));
                            context.Response.ContentType = Resource.ContentType;
                            context.Response.Flush();
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                    }
                }
            }
        }

        /// <summary>
        /// Converts the specified string into an ASCII-encoded byte array.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>The ASCII byte array.</returns>
        public static byte[] StringToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        #endregion
    }
}
