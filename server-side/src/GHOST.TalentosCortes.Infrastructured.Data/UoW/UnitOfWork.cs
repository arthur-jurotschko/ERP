using GHOST.TalentosCortes.Domain.Interfaces;
using GHOST.TalentosCortes.Infrastructured.Data.Context;


namespace GHOST.TalentosCortes.Infrastructured.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly TalentosCortesContext _context;

        public UnitOfWork(TalentosCortesContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}