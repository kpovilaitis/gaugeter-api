namespace CarGaugesApi.Authentication
{
    public interface ITokenFactory
    {
        string GetAuthToken(string userId, string secret);

        string GetRefreshToken(int size = 32);
    }
}
