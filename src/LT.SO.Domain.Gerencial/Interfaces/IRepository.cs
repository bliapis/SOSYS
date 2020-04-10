using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using LT.SO.Domain.Core.Models;

namespace LT.SO.Domain.Gerencial.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        void Add(TEntity obj);
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity obj);
        void Remove(Guid id);
        int SaveChanges();
    }
}