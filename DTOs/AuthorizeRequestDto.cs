using System.ComponentModel.DataAnnotations;

namespace ExploreCalifornia.DTOs
{
    public class AuthorizeRequestDto
    {   
        [Required]
        [MinLength(32), MaxLength(32)]
        public string AppToken { get; set; }

        [Required]
        [MinLength(32), MaxLength(32)]
        public string AppSecret { get; set; }
    }
}