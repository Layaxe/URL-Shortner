using URL_Shortner.DTOs;
using URL_Shortner.Models;
using URL_Shortner.Repositories;

namespace URL_Shortner.Services
{
    public class UrlService
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IShortUrlRepository _shortUrlRepository;

        public UrlService(IIdGenerator idGenerator, IShortUrlRepository shortUrlRepository)
        {
            _idGenerator = idGenerator;
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<string> CreateAsync(string longUrl)
        {
            // We validate the URL in the service so it can be tested later without controller.
            if (!Uri.TryCreate(longUrl, UriKind.Absolute, out var uri) || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException("URL must use the HTTP/S scheme.", nameof(uri));
            }

            var id = _idGenerator.CreateId();
            var code = Base62Encoder.Encode(id);

            ShortUrl shortUrl = new() { Code = code , LongUrl = longUrl};

            await _shortUrlRepository.AddAsync(shortUrl);

            return code;
        }

        public async Task<string?> GetUrlAsync(string code)
        {
            return await _shortUrlRepository.GetUrlByCodeAsync(code);
        }
    }
}
