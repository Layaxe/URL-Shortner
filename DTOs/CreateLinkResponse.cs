using System.ComponentModel.DataAnnotations;

namespace URL_Shortner.DTOs
{
    public class CreateLinkResponse
    {
        [Required]
        public string ShortUrl { get; set; } = string.Empty;
    }
}
