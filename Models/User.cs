using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace API.Models
{
    public partial class User
    {
        [Key]
        public string UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [MaxLength(200)]
        public string Password { get; set; }
        public string Email { get; set; }

    }
}

//* พิมพ์ prop เพิ่อเรียกใช้แม่แบบ get set 