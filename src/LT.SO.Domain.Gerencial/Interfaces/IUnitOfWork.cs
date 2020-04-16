using System;
using LT.SO.Domain.Core.Commands;

namespace LT.SO.Domain.Gerencial.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}