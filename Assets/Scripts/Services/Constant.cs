namespace Services
{
    public class Constant
    {
        public string GetFullUrl(string path)
        {
            return $"http://localhost:5157{path}";
        }
    }
}