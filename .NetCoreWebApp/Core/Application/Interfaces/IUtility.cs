namespace Application.Interfaces
{
    public interface IUtility
    {
        T DeepCopy<T>(T obj);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
