namespace Application.Dto
{
    public class UserResponseDto : ApiResponse<UserData>
    {
        public UserResponseDto(bool success, string message, UserData data) : base(success, message, data)
        {
        }
    }

    public class UserData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MobilePhoneNumber { get; set; }
        public long Tckn { get; set; }
        public string Gender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserData()
        {
        }
        public UserData(int id, string name, string surname, string mobilePhoneNumber, long tckn, string gender, string userName, string password)
        {
            Id = id;
            Name = name;
            Surname = surname;
            MobilePhoneNumber = mobilePhoneNumber;
            Tckn = tckn;
            Gender = gender;
            UserName = userName;
            Password = password;
        }
    }
}