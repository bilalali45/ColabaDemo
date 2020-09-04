﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LosIntegration.API.Models.Document
{
    [DataContract(Name = "documents")]
    public class DocumentType
    {
        [DataMember(Name = "docTypeId")]
        public string DocTypeId { get; set; }
        [DataMember(Name = "docType")]
        public string DocType { get; set; }
        [DataMember(Name = "docMessage")]
        public string DocMessage { get; set; }
    }
    [DataContract]
    public class DocumentCategory
    {
        [DataMember(Name = "catId")]
        public string CatId { get; set; }
        [DataMember(Name = "catName")]
        public string CatName { get; set; }
        [DataMember(Name = "documents")]
        public List<DocumentType> DocumentTypes { get; set; }
    }

}