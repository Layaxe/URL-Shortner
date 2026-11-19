namespace URL_Shortner.Repositories
{
    public interface IShortUrlRepository
    {
        public void AddNewUrl(long code, string url);
        public string GetUrl(long code);

    }
}
