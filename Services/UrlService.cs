using URL_Shortner.DTOs;
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

        public string CreateShortCode(CreateLinkRequest request)
        {
            var id = _idGenerator.CreateId();
            var code = Base62Encoder.Encode(id);

            _shortUrlRepository.AddNewUrl(code, request.LongUrl);

            return code;
        }

        public string? GetUrl(string code)
        {
            return _shortUrlRepository.GetUrl(code);
        }
    }
}
