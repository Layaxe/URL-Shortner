using System.ComponentModel.DataAnnotations;

namespace URLShortner.DTOs
{
    public class CreateLinkResponse
    {
        [Required]
        public string ShortUrl { get; set; } = string.Empty;
    }
}
