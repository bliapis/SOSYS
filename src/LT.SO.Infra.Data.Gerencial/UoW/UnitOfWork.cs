using LT.SO.Domain.Core;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Infra.Data.Gerencial.Context;

namespace LT.SO.Infra.Data.Gerencial.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GerencialContext _context;

        public UnitOfWork(GerencialContext context)
        {
            _context = context;
        }

        public CommandResponse Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}