using System;
using LT.SO.Domain.Core.Commands;

namespace LT.SO.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}