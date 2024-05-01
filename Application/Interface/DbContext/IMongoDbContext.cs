using MongoDB.Driver;

namespace Application.Interface.DbContext
{
    public interface IMongoDbContext<T>
    {
        public IMongoCollection<T> GetCollection();
    }
}
