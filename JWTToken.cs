namespace Notes
{
    public class JWTToken
    {
        public static string secret_key = Environment.GetEnvironmentVariable("SECRET_KEY");
    }
}