using System.ComponentModel.DataAnnotations;

namespace URLShortner.DTOs
{
    public class CreateLinkRequest
    {
        [Required]
        public string LongUrl { get; set; } = string.Empty;
    }
}
