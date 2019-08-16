using System;
using System.ComponentModel.DataAnnotations;

namespace ExploreCalifornia.DataAccess.Models
{
    public class AuthorizedApp
    {
        public int AuthorizedAppId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string AppToken { get; set; }

        [Required]
        [StringLength(32)]
        public string AppSecret { get; set; }

        public DateTime TokenExpiration { get; set; }
    }
}
