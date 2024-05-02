namespace Application.Interfaces
{
    public interface IUtility
    {
        TU DeepCopy<T, TU>(T source, TU dest);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        bool IsCoordinateInsideCircle(double lat1, double lon1, double lat2, double lon2, double radius);
    }
}