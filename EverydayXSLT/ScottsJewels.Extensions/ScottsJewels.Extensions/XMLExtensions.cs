using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace ScottsJewels
{
    /// <summary>
    /// Extensions to the .NET Framework.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts an object into an XML string.
        /// </summary>        
        /// <returns>An XML string representing the object.</returns>
        public static string ToXml(this Object obj)
        {
            StringBuilder str = new StringBuilder();

            using (StringWriter writer = new StringWriter(str))
            {
                XmlSerializer x = new XmlSerializer(obj.GetType());
                x.Serialize(writer, obj);
            }

            return str.ToString();

        }

        /// <summary>
        /// Transforms an object using the specified XSL Transformation.
        /// </summary>        
        /// <param name="xslTransformFilePath">The path to the XSL Transform file.</param>
        /// <returns>The transformed object.</returns>
        public static string XslTransform(this Object obj, string xslTransformFilePath)
        {
            string result = string.Empty;

            if (obj != null)
            {
                string xml;

                xml = obj.ToXml();

                if (File.Exists(xslTransformFilePath))
                {
                    XslCompiledTransform xslt = new XslCompiledTransform();

                    xslt.Load(xslTransformFilePath);

                    using (StringReader xmlStream = new StringReader(xml))
                    {
                        using (StringWriter stringWriter = new StringWriter())
                        {
                            using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { ConformanceLevel = ConformanceLevel.Fragment }))
                            {
                                xslt.Transform(XmlReader.Create(xmlStream), xmlWriter);

                                result = stringWriter.ToString();
                            }

                        }

                    }

                }
            }

            return result;
        }
    }
}
