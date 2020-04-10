using LT.SO.Domain.Core.Models;
using System;

namespace LT.SO.Infra.CrossCutting.Log.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
    {
        void Add(TEntity obj);
        int SaveChanges();
    }
}
