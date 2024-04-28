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
        public int Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string MobilePhoneNumber { get; }
        public long Tckn { get; }
        public string Gender { get; }
        public string UserName { get; }
        public string Password { get; }

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

        // Additional methods or properties related to user data can be added here
    }
}
