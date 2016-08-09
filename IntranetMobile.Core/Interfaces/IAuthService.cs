using System.Threading.Tasks;
using IntranetMobile.Core.Models.Dtos;

namespace IntranetMobile.Core.Interfaces
{
    public interface IAuthService
    {
		Task <AuthDto> Login(string email, string paswword);
		Task Logout();
		Task ResetPassword(string email);
    }
}