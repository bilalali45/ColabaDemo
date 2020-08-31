using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentManagement.Service
{
    public interface IMongoService
    {
        IMongoDatabase db { get; set; }
        IMongoClient client { get; set; }
    }
}
