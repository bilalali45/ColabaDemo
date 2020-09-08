using System;
using System.Data;
using System.IO;

namespace RainMaker.Common.Extensions
{
    public static class DatasetExtension
    {
        public static string ToXml(this DataSet ds)
        {
            var ms = new StringWriter();
            ds.WriteXml(ms,XmlWriteMode.WriteSchema);
            //ds.GetXml();
           
            
            return Convert.ToString(ms);
          
        }
    }
}
