using System.Collections.Concurrent;
using URL_Shortner.DTOs;

namespace URL_Shortner.Services
{
    public class UrlService
    {
        private readonly IIdGenerator _idGenerator;

        public UrlService(IIdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        public string CreateShortCode(CreateLinkRequest request)
        {
            var id = _idGenerator.CreateId();
            var code = Base62Encoder.Encode(id);

            return code;
        }
    }
}
