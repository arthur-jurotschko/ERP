using GHOST.TalentosCortes.Domain.Entities.MasterUsers;
using GHOST.TalentosCortes.Domain.Entities.MasterUsers.Repository;
using GHOST.TalentosCortes.Infrastructured.Data.Context;

namespace GHOST.TalentosCortes.Infrastructured.Data.Repository
{
    public class MasterUserRepository : Repository<MasterUser>, IMasterUserRepository
    {
        public MasterUserRepository(TalentosCortesContext context) : base(context)
        {
        }
    }
}
