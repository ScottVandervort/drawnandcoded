using System.Collections.Generic;

namespace ScottsJewels.Web.UI
{
    /// <summary>
    /// The Style Comparer allows for two Styles to be compared without modifying the Style class.
    /// </summary>
    public class StyleComparer : EqualityComparer<Style>
    {
        #region Methods
        public override bool Equals(Style x, Style y)
        {
            return (((x.Media ?? string.Empty).Trim().ToUpper() == (y.Media ?? string.Empty).Trim().ToUpper()) &&
                    ((x.Path ?? string.Empty).Trim().ToUpper() == (y.Path ?? string.Empty).Trim().ToUpper()));
        }

        public override int GetHashCode(Style obj)
        {
            return string.Format("{0}{1}",
                (obj.Media ?? string.Empty).Trim().ToUpper(),
                (obj.Path ?? string.Empty).Trim().ToUpper()).GetHashCode();
        }
        #endregion
    }
}
