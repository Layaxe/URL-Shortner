using System.ComponentModel.DataAnnotations;

namespace URL_Shortner.DTOs
{
    public class CreateLinkRequest
    {
        [Required]
        public string LongUrl { get; set; } = string.Empty;
    }
}
