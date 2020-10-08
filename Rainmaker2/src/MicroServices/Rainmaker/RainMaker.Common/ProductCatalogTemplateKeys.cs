using System.Collections.Generic;
using System.Linq;

namespace RainMaker.Common
{
    public enum CatalogKey
    {
        CityName,
        StateName,
    }

    public class ProductCatalogTemplateKeys
    {
        private static readonly IDictionary<CatalogKey, ProductCatalogTemplateKey> Keys = new Dictionary<CatalogKey, ProductCatalogTemplateKey>
        {
            {CatalogKey.CityName,   new ProductCatalogTemplateKey{KeyName = "City" ,Symbol = "###City###",Description = "City Name",IsIndependentSystemKey=true}},
            {CatalogKey.StateName,   new ProductCatalogTemplateKey{KeyName = "State",Symbol = "###State###",Description = "State Name",IsIndependentSystemKey=true}},
        };


        public static string GetKeySymbol(CatalogKey key)
        {
            var tem = Keys[key];
            return tem != null ? tem.Symbol : key.ToString();
        }
        public static IEnumerable<KeyValuePair<CatalogKey, ProductCatalogTemplateKey>> GetValueWithKeys(int entityRefTye)
        {
            return Keys.Where(s => s.Value.EntityRefType == entityRefTye || s.Value.IsIndependentSystemKey);
        }
        public static IEnumerable<ProductCatalogTemplateKey> GetKeys()
        {
            return Keys.Values;
        }

        public static IEnumerable<KeyValuePair<CatalogKey, ProductCatalogTemplateKey>> GetIndependentSystemKeyKeys()
        {
            return Keys.Where(s => s.Value.IsIndependentSystemKey);
        }

    }

    public class ProductCatalogTemplateKey
    {
        public string KeyName { get; set; }
        public string Description { get; set; }
        public string Symbol { get; set; }
        public int EntityRefType { get; set; }
        public TemplateCommand? GeneratingCommand { get; set; }
        public bool IsIndependentSystemKey { get; set; }

    }



}
