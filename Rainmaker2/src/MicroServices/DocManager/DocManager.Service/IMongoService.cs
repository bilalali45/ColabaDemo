using MongoDB.Driver;

namespace DocManager.Service
{
    public interface IMongoService
    {
        IMongoDatabase db { get; set; }
    }
}
