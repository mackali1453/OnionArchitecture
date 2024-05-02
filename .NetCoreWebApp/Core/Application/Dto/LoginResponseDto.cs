using Application.Dto;

namespace Github.NetCoreWebApp.Core.Applications.Dto
{
    public class LoginResponseDto : ApiResponse<LoginResponse>
    {
        public LoginResponseDto(bool success, string message, LoginResponse data) : base(success, message, data)
        {
        }
    }
    public class LoginResponse
    {
        public string AccessToken { get; set; }
    }
}
