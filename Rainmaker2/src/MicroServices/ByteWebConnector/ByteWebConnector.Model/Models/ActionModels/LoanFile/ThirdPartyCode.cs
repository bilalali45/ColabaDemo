













using System.Collections.Generic;
using System.Linq;

namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ThirdPartyCode

    public partial class ThirdPartyCode 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ThirdPartyId { get; set; } // ThirdPartyId
        public string ElementName { get; set; } // ElementName
        public string Code { get; set; } // Code (length: 300)
        public int? EntityRefTypeId { get; set; } // EntityRefTypeId
        public int? EntityRefId { get; set; } // EntityRefId

        // Foreign keys

        /// <summary>
        /// Parent EntityType pointed by [ThirdPartyCode].([EntityRefTypeId]) (FK_ThirdPartyCode_EntityType)
        /// </summary>
        public virtual EntityType EntityType { get; set; } // FK_ThirdPartyCode_EntityType

        public ThirdPartyCode()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

    public class ThirdPartyCodeList
    {
        public List<ThirdPartyCode> ThirdPartyCodes { get; set; }


        public string GetByteProValue(string elementName,
                                      int? entityRefId
        )
        {
            var value = "";
            if (entityRefId != null) value = ThirdPartyCodes.FirstOrDefault(predicate: rec => rec.EntityRefId == entityRefId && rec.ElementName == elementName)?.Code;
            return value;
        }


    }

}
// </auto-generated>
