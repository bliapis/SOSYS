using LT.SO.Domain.Core.Commands;
using System;

namespace LT.SO.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}