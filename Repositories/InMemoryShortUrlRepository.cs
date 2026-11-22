using System.Collections.Concurrent;
using URL_Shortner.Models;

namespace URL_Shortner.Repositories
{
    public class InMemoryShortUrlRepository : IShortUrlRepository
    {
        private static readonly ConcurrentDictionary<string, string> Urls = new(); // Concurrent Dici because multiple request's can run at the same time
        public async Task AddAsync(ShortUrl shortUrl)
        {
            Urls[shortUrl.Code] = shortUrl.LongUrl;
        }

        public async Task<string?> GetUrlByCodeAsync(string code)
        {
            if (Urls.TryGetValue(code, out var longUrl)) { return longUrl; }
            return null;
        }
    }
}
