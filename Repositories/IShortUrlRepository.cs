using URL_Shortner.Models;

namespace URL_Shortner.Repositories
{
    public interface IShortUrlRepository
    {
        public Task AddAsync(ShortUrl shortUrl);
        public Task<string?> GetUrlByCodeAsync(string code);

    }
}
