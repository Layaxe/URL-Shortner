using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using URL_Shortner.DTOs;

namespace URL_Shortner.Controllers
{
    [Route("api/[controller]")] // api/links
    [ApiController]
    public class LinksController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, string> Links = new(); // Concurrent Dici because multiple request's can run at the same time

        [HttpPost]
        public IActionResult CreateShortURL([FromBody] CreateLinkRequest request)
        {
            // This check is curcial since a user could input an Url without http or https which will result in bad redirect so we check that first
            if (!Uri.TryCreate(request.LongUrl, UriKind.Absolute, out var uri) || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                return BadRequest("Url must be a valid http or https address.");
            }

            var code = Guid.NewGuid().ToString("N")[..7]; // This will be routed to a service, and we will use snowflake id with base62 so no collisions

            Links[code] = request.LongUrl;

            var shortUrl = $"{Request.Scheme}://{Request.Host}/{code}";

            return Ok(new CreateLinkResponse { ShortUrl = shortUrl });
        }

        [HttpGet("/{code}")]
        public IActionResult RedirectURL(string code)
        {
            if (Links.TryGetValue(code, out var longUrl)) { 
                return Redirect(longUrl); // Return 302 which is temp redirect you can also 301 which is permenant redirect both works but one useful for analysis the other not.
            }
            return NotFound();
        }
    }
}
