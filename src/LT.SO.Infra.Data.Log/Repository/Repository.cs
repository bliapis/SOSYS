using LT.SO.Domain.Core.Models;
using LT.SO.Infra.CrossCutting.Log.Interfaces;
using LT.SO.Infra.Data.Log.Context;
using Microsoft.EntityFrameworkCore;

namespace LT.SO.Infra.Data.Log.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected LogContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(LogContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}