using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.MongoDb.Concrete.Configurations;
using Core.Entities;
using Core.Utilities.Messages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Core.DataAccess.MongoDb.Concrete
{
    public abstract class MongoDbRepositoryBase<T> : IDocumentDbRepository<T>
        where T : DocumentDbEntity
    {
        private readonly IMongoCollection<T> _collection;
        protected string CollectionName { get; set; }
        protected MongoDbRepositoryBase(MongoConnectionSettings mongoConnectionSetting, string collectionName)
        {
            CollectionName = collectionName;

            ConnectionSettingControl(mongoConnectionSetting);


            var client = mongoConnectionSetting.GetMongoClientSettings() == null
                ? new MongoClient(mongoConnectionSetting.ConnectionString)
                : new MongoClient(mongoConnectionSetting.GetMongoClientSettings());


            var database = client.GetDatabase(mongoConnectionSetting.DatabaseName);
            _collection = database.GetCollection<T>(collectionName);
        }

        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            var data = predicate == null
                ? _collection.AsQueryable()
                : _collection.AsQueryable().Where(predicate);

            return data.FirstOrDefault() != null;
        }

        public virtual void Delete(ObjectId id)
        {
            _collection.FindOneAndDelete(x => x.Id == id);
        }

        public virtual void Delete(T record)
        {
            _collection.FindOneAndDelete(x => x.Id == record.Id);
        }

        public virtual async Task DeleteAsync(ObjectId id)
        {
            await _collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public virtual async Task DeleteAsync(T record)
        {
            await _collection.FindOneAndDeleteAsync(x => x.Id == record.Id);
        }

        public virtual T GetById(ObjectId id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public virtual async Task<T> GetByIdAsync(ObjectId id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual void Add(T entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            _collection.InsertOne(entity, options);
        }

        public virtual async Task AddAsync(T entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await _collection.InsertOneAsync(entity, options);
        }

        public virtual void AddMany(IEnumerable<T> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            _collection.BulkWriteAsync((IEnumerable<WriteModel<T>>)entities, options);
        }

        public virtual async Task AddManyAsync(IEnumerable<T> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            await _collection.BulkWriteAsync((IEnumerable<WriteModel<T>>)entities, options);
        }

        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
                ? _collection.AsQueryable()
                : _collection.AsQueryable().Where(predicate);
        }

        public virtual async Task<IQueryable<T>> GetListAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await Task.Run(() =>
            {
                return predicate == null
                    ? _collection.AsQueryable()
                    : _collection.AsQueryable().Where(predicate);
            });
        }

        public virtual void Update(ObjectId id, T record)
        {
            _collection.FindOneAndReplace(x => x.Id == id, record);
        }

        public virtual void Update(T record, Expression<Func<T, bool>> predicate)
        {
            _collection.FindOneAndReplace(predicate, record);
        }

        public virtual async Task UpdateAsync(ObjectId id, T record)
        {
            await _collection.FindOneAndReplaceAsync(x => x.Id == id, record);
        }

        public virtual async Task UpdateAsync(T record, Expression<Func<T, bool>> predicate)
        {
            await _collection.FindOneAndReplaceAsync(predicate, record);
        }

        private void ConnectionSettingControl(MongoConnectionSettings settings)
        {
            if (settings.GetMongoClientSettings() != null &&
                (string.IsNullOrEmpty(CollectionName) || string.IsNullOrEmpty(settings.DatabaseName)))
            {
                throw new Exception(DocumentDbMessages.NullOremptyMessage);
            }

            if (string.IsNullOrEmpty(CollectionName) ||
                string.IsNullOrEmpty(settings.ConnectionString) ||
                string.IsNullOrEmpty(settings.DatabaseName))
            {
                throw new Exception(DocumentDbMessages.NullOremptyMessage);
            }
        }
    }
}