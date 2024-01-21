namespace Github.NetCoreWebApp.Core.Applications.Dto
{
    public class CheckUserResponseDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public int LogInTryCount { get; set; }
        public bool IsLockedOut { get; set; }
        public string AccessToken { get; set; }
    }
}
