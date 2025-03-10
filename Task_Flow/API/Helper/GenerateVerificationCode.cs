namespace API.Helper
{
    public static class GenerateVerificationCode
    {
        public static string GenerateRandomCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
