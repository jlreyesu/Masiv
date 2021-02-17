using System.ComponentModel.DataAnnotations;

namespace Masiv.Core.DTOs
{
    public class UserSessionDTO
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public string State { get; set; }
        public bool Active { get; set; }
    }
}
