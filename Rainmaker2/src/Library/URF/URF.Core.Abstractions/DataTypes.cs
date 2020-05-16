using System.Collections.Generic;


namespace RainMaker.Common
{
    public class DynamicLinQFilter
    {
        public DynamicLinQFilter()
        {
            Sort = "";
            Filter = "";
            Predicates = new List<object>();
        }
        public string Sort { get; set; }
        public string Filter { get; set; }

        public List<object> Predicates;
    }
   
    
}
