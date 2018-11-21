using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace ScottsJewels.Web.UI
{
    /// <summary>
    /// A Script (JS) Resource Manager.
    /// </summary>
    public class ScriptManager : ResourceManager<Script, ScriptComparer>
    {
        #region Methods
        /// <summary>
        /// Renders the resources managed by this control.
        /// </summary>
        /// <param name="output">A HtmlTextWriter that represents the output stream to render HTML content on the client.</param>
        /// <param name="resources">The non-composite resources to render.</param>
        /// <param name="compositeResources">The composite resources to render.</param>
        protected override void RenderResources(HtmlTextWriter output, List<Script> resources, List<Script> compositeResources)
        {
            foreach (Script script in resources)
            {
                output.WriteLine(
                    string.Format(
                        "<script src=\"ScriptHttpHandler.ashx?Resource={0}\" type=\"{1}\" ></script>",
                        script.Path,
                        script.ContentType));
            }

            if (compositeResources.Count > 0)
            {
                StringBuilder compositeResourcePaths = new StringBuilder();

                for (int scriptIndex = 0; scriptIndex < compositeResources.Count; scriptIndex++)
                {
                    compositeResourcePaths.Append(compositeResources[scriptIndex].Path);

                    if (scriptIndex < compositeResources.Count - 1)
                        compositeResourcePaths.Append(",");
                }

                output.WriteLine(
                    string.Format(
                        "<script src=\"ScriptHttpHandler.ashx?Resource={0}\" type=\"{1}\" ></script>",
                        compositeResourcePaths,
                        compositeResources[0].ContentType));
            }
        }
        #endregion
    }
}
