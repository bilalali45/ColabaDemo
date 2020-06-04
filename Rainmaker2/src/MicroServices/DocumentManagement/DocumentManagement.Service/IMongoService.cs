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

    public interface IMongoServiceAggregate<T>
    {
        IAggregateFluent<BsonDocument> Unwind(IAggregateFluent<T> aggregateFluent,FieldDefinition<T> field,AggregateUnwindOptions<BsonDocument> options=null);
        IAggregateFluent<BsonDocument> Lookup(IAggregateFluent<T> aggregateFluent,string foreignCollection, FieldDefinition<T> localField, FieldDefinition<BsonDocument> foreignField, FieldDefinition<BsonDocument> @as);
        IAggregateFluent<BsonDocument> Project(IAggregateFluent<T> aggregateFluent,ProjectionDefinition<T, BsonDocument> projection);
    }
}
