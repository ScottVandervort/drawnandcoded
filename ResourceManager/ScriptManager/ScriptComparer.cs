using System.Collections.Generic;

namespace ScottsJewels.Web.UI
{
    /// <summary>
    /// The Script Comparer allows for two Scripts to be compared without modifying the Script class.
    /// </summary>
    public class ScriptComparer : EqualityComparer<Script>
    {
        #region Methods
        public override bool Equals(Script x, Script y)
        {
            return ((x.Path ?? string.Empty).Trim().ToUpper() == (y.Path ?? string.Empty).Trim().ToUpper());
        }

        public override int GetHashCode(Script obj)
        {
            return string.Format("{0}",
                (obj.Path ?? string.Empty).Trim().ToUpper()).GetHashCode();
        }
        #endregion
    }
}
