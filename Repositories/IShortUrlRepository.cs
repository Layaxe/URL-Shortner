namespace URL_Shortner.Repositories
{
    public interface IShortUrlRepository
    {
        public Task AddAsync(string code, string url);
        public Task<string?> GetUrlByCodeAsync(string code);

    }
}
