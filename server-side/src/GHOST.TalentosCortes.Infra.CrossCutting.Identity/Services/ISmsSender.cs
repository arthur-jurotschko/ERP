using System.Threading.Tasks;

namespace GHOST.TalentosCortes.Infra.CrossCutting.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}