using URLShortner.Models;

namespace URLShortner.Repositories
{
    public interface IShortUrlRepository
    {
        public Task AddAsync(ShortUrl shortUrl);
        public Task<string?> GetUrlByCodeAsync(string code);

    }
}
