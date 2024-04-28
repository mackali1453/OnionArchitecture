using Github.NetCoreWebApp.Core.Applications.Dto;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Authenticate(string password, string username);
    }
}