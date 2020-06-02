using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Web;
using Core.Models;

namespace ChatApp.Web.Repos
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseDBClass
    {
        protected readonly AppDbContext _dbContext = new AppDbContext();

        public DbSet<TEntity> tableContext => _dbContext.Set<TEntity>();

        public IQueryable<TEntity> Set => tableContext;
        public TEntity Get(long id)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(a => a.Id == id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }
        public TEntity Add(TEntity entity)
        {
            var a = _dbContext.Set<TEntity>();

            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

    }

    public interface IRepository<TEntity>
    {
        //Set
        IQueryable<TEntity> Set { get; }

        //Get
        TEntity Get(long id);

        //Getall
        IEnumerable<TEntity> GetAll();

        //Add
        TEntity Add(TEntity entity);

        //Update
        void Update(TEntity entity);

        //Delete
        void Delete(TEntity entity);

        //Save changes
        void SaveChanges();
    }

}
