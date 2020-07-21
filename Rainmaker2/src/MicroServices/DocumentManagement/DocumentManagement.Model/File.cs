using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Model
{
    public class FileNameModel
    {
        public string fileName { get; set; }
        public int order { get; set; }
    }
    public class DoneModel
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
        public int tenantId { get; set; }

    }

    public class FileViewModel
    {
        [FromQuery(Name = "id")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string id { get; set; }

        [FromQuery(Name = "requestId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string requestId { get; set; }

        [FromQuery(Name = "docId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string docId { get; set; }

        [FromQuery(Name = "fileId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        [RegularExpression(@"^[A-Fa-f\d]{24}$", ErrorMessage = "Validation Failed")]
        public string fileId { get; set; }

        [FromQuery(Name = "tenantId")]
        [Required(ErrorMessage = "Field Can't be empty")]
        public int tenantId { get; set; }
    }
    public class FileRenameModel
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
        public string fileId { get; set; }
        public string fileName { get; set; }
        public int tenantId { get; set; }
    }

    public class FileOrderModel
    {
        public string id { get; set; }
        public string docId { get; set; }
        public string requestId { get; set; }
        public List<FileNameModel> files { get; set; }
        public int tenantId { get; set; }
    }

   

}
