using System.Linq.Expressions;
using MongoDB.Driver;

namespace Common.MongoDB;

public class MongoRepository<T> : IRepository<T>
    where T : IEntity
{
    private readonly IMongoCollection<T> dbCollection;
    private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        dbCollection = database.GetCollection<T>(collectionName);
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await dbCollection.Find(filter).ToListAsync();
    }

    public async Task<T> GetAsync(Guid id)
    {
        FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
        return await dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
    {
        return await dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<T> CreateAsync(T user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        await dbCollection.InsertOneAsync(user);
        return user;
    }

    public async Task<T> UpdateAsync(T user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        FilterDefinition<T> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, user.Id);
        await dbCollection.ReplaceOneAsync(filter, user);
        return user;
    }

    public async Task RemoveAsync(Guid id)
    {
        FilterDefinition<T> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, id);
        await dbCollection.DeleteOneAsync(filter);
    }
}
