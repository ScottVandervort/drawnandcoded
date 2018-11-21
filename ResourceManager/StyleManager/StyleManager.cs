using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace ScottsJewels.Web.UI
{
    /// <summary>
    /// A Stylesheet (CSS) Resource Manager.
    /// </summary>
    public class StyleManager : ResourceManager<Style, StyleComparer>
    {
        #region Methods
        /// <summary>
        /// Renders the resources managed by this control.
        /// </summary>
        /// <param name="output">A HtmlTextWriter that represents the output stream to render HTML content on the client.</param>
        /// <param name="resources">The non-composite resources to render.</param>
        /// <param name="compositeResources">The composite resources to render.</param>
        protected override void RenderResources(HtmlTextWriter output, List<Style> resources, List<Style> compositeResources)
        {
            foreach (Style style in resources)
            {
                output.WriteLine(
                    string.Format(
                        "<link media=\"{0}\" href=\"StyleHttpHandler.ashx?Resource={1}\" type=\"{2}\" rel=\"stylesheet\" />",
                        style.Media,
                        style.Path,
                        style.ContentType));
            }

            if (compositeResources.Count > 0)
            {
                // We don't want to combine styles for different Media's (screen, print, etc...). Let's group the composite resources by Media prior top rendering.  
                List<IGrouping<string, Style>> compositeStylesByMedia = compositeResources.GroupBy(a => a.Media.Trim().ToUpper()).ToList();
                StringBuilder compositeResourcePaths = new StringBuilder();
                List<Style> mediaStyles;

                for (int mediaIndex = 0; mediaIndex < compositeStylesByMedia.Count; mediaIndex++)
                {
                    mediaStyles = compositeStylesByMedia[mediaIndex].ToList();

                    for (int styleIndex = 0; styleIndex < mediaStyles.Count; styleIndex++)
                    {
                        compositeResourcePaths.Append(mediaStyles[styleIndex].Path);

                        if (styleIndex < mediaStyles.Count - 1)
                            compositeResourcePaths.Append(",");
                    }

                    output.WriteLine(
                        string.Format(
                            "<link media=\"{0}\" href=\"StyleHttpHandler.ashx?Resource={1}\" type=\"text/css\" rel=\"Stylesheet\" />",
                            mediaStyles[0].Media,
                            compositeResourcePaths));

                    compositeResourcePaths.Clear();
                }
            }
        }
        #endregion
    }
}
