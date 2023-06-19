using System.ComponentModel.DataAnnotations;

namespace backend.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public int Age { get; set; }

        [Required]
        public string Password { get; set; }

        public string Phone { get; set; }
    }
}
