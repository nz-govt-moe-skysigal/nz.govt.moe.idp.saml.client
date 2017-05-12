using NZ.MOE.IdP.SAML.Client.Schema.Core;

namespace NZ.MOE.IdP.SAML.Client.Profiles.DKSaml20.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class DKSaml20SpecVerAttribute : DKSaml20Attribute
    {
        /// <summary>
        /// Attribute name
        /// </summary>
        public const string NAME = "dk:gov:saml:attribute:SpecVer";

        /// <summary>
        /// Creates an attribute with the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SamlAttribute Create(string value)
        {
            return Create(NAME, null, value);
        }
    }
}