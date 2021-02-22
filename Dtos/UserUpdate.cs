using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public partial class UserUpdateDto
    {

        [Required]
        public string UserName { get; set; }
        [MaxLength(200)]
        public string Password { get; set; }
        public string Email { get; set; }

    }
}