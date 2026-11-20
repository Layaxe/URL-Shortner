using System.Collections.Concurrent;

namespace URL_Shortner.Repositories
{
    public class InMemoryShortUrlRepository : IShortUrlRepository
    {
        private static readonly ConcurrentDictionary<string, string> Urls = new(); // Concurrent Dici because multiple request's can run at the same time
        public void AddNewUrl(string code, string url)
        {
            Urls[code] = url;
        }

        public string? GetUrl(string code)
        {
            if (Urls.TryGetValue(code, out var longUrl)) { return longUrl; }
            return null;
        }
    }
}
