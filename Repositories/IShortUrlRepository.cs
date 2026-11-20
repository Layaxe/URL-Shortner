namespace URL_Shortner.Repositories
{
    public interface IShortUrlRepository
    {
        public void AddNewUrl(string code, string url);
        public string? GetUrl(string code);

    }
}
