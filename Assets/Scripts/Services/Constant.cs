namespace Services
{
    public static class Constant
    {
        public static string SignInUrl { get; } = GetFullUrl("auth/signIn");
        public static string SignUpUrl { get; } = GetFullUrl("auth/signUp");
        
        private static string GetFullUrl(string path)
        {
            return $"http://localhost:5157/{path}";
        }
       
    }
}