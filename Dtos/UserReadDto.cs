using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public partial class UserReadDto
    {
        [Key]
        public string UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}