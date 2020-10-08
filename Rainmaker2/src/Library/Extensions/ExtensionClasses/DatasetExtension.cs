using System;
using System.Data;
using System.IO;

namespace Extensions.ExtensionClasses
{
    public static class DatasetExtension
    {
        public static string ToXml(this DataSet ds)
        {
            var ms = new StringWriter();
            ds.WriteXml(ms,XmlWriteMode.WriteSchema);
            
            return Convert.ToString(ms);
          
        }
    }
}
