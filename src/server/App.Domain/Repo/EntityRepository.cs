using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using App.Utils;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace App.Domain.Repo
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MongoCollection<TEntity> _collection;

        public EntityRepository()
        {
            var mongoCnnStr = ConfigurationManager.AppSettings[ConstHelper.MongoCnnStr] ?? "mongodb://localhost";
            var dbName = ConfigurationManager.AppSettings[ConstHelper.MongoDBName] ?? "TestDB";
            var concern = new WriteConcern { Journal = true, W = 1 };

            var mongoDatabase = new MongoClient(mongoCnnStr).GetServer().GetDatabase(dbName);

            _collection = mongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name, concern);
        }

        public MongoCursor<TEntity> FindAll()
        {
            return _collection.FindAllAs<TEntity>();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _collection.AsQueryable().Where(x => !x.IsDeleted);
        }

        public IQueryable<TEntity> AsOrderedQueryable()
        {
            return AsQueryable().OrderByDescending(x => x.Id);
        }

        public WriteConcernResult Add(TEntity entity)
        {
            return _collection.Insert(entity);
        }

        public void AddBulk(IEnumerable<TEntity> entities)
        {
            _collection.InsertBatch(entities);
        }

        public WriteConcernResult Delete(TEntity entity)
        {
            return _collection.Update(
                Query<TEntity>.EQ(x => x.Id, entity.Id),
                Update<TEntity>.Set(x => x.DeletedAt, entity.DeletedAt)
                               .Set(x => x.DeletedBy, entity.DeletedBy)
                               .Set(x => x.IsDeleted, true));
        }

        public WriteConcernResult Update(IMongoQuery mongoQuery, IMongoUpdate mongoUpdate)
        {
            return Update(mongoQuery, mongoUpdate, UpdateFlags.Upsert);
        }

        public WriteConcernResult UpdateMulti(IMongoQuery mongoQuery, IMongoUpdate mongoUpdate)
        {
            return Update(mongoQuery, mongoUpdate, UpdateFlags.Multi);
        }

        public WriteConcernResult Update(IMongoQuery mongoQuery, IMongoUpdate mongoUpdate, UpdateFlags flag)
        {
            return _collection.Update(mongoQuery, mongoUpdate, flag);
        }

        public WriteConcernResult Save(TEntity doc)
        {
            return _collection.Save(doc);
        }
        public WriteConcernResult DeleteAll()
        {
            return _collection.RemoveAll();
        }
    }
}