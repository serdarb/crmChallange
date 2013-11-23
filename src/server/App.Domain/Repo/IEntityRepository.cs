using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace App.Domain.Repo
{
    public interface IEntityRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Gets all the data in collection. 
        /// We have this method in repository but we do not want it to be used in services!!!
        /// It can create performance problems as application grows up.
        /// </summary>
        /// <returns></returns>
        MongoCursor<TEntity> FindAll();


        /// <summary>
        /// To query the collections we must use this method.
        /// Our soft delete logic is handled from BaseEntity's DeletedOn field.
        /// This method encapsulates the need to adding Where(x => x.DeletedOn == null) all the time.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// Encapsulates the need of OrderByDescending(x => x.CreatedOn).
        /// Gets a quariable object for the collection which is ordered by descanding with entity creation time.
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsOrderedQueryable();

        /// <summary>
        /// Adds the entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        WriteConcernResult Add(TEntity entity);

        /// <summary>
        /// To bulk insert into collection
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void AddBulk(IEnumerable<TEntity> entities);

        /// <summary>
        /// soft deletes the entity.
        /// Queries by id. And Updates the DeletedOn and DeletedBy fields.
        /// </summary>
        /// <param name="entity">Id, DeletedOn and DeletedBy fields must be filled</param>
        /// <returns></returns>
        WriteConcernResult Delete(TEntity entity);

        /// <summary>
        /// Updates entities with given query and update actions.
        /// </summary>
        /// <param name="mongoQuery"></param>
        /// <param name="mongoUpdate"></param>
        /// <returns></returns>
        WriteConcernResult Update(IMongoQuery mongoQuery, IMongoUpdate mongoUpdate);
        WriteConcernResult Update(IMongoQuery mongoQuery, IMongoUpdate mongoUpdate, UpdateFlags flag);
        WriteConcernResult UpdateMulti(IMongoQuery mongoQuery, IMongoUpdate mongoUpdate);

        WriteConcernResult Save(TEntity doc);
    }
}