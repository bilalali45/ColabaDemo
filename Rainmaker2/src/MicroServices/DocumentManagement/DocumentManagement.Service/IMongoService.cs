using MongoDB.Driver;

namespace DocumentManagement.Service
{
    public interface IMongoService
    {
        IMongoDatabase db { get; set; }
    }
}
