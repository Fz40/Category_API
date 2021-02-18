using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public partial class CategoryUpdateDto
    {
        [Required]
        public string CategoryName { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }

    }
}