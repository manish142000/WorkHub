using System.ComponentModel.DataAnnotations;

namespace backend.Models.Dto
{
    public class OrderDto
    {
        [Required]
        public string UserEmail { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateCreated { get; set; }

        [Required]
        public DayOfWeek DayCreated { get; set; }

        [Required]
        public string Lunch { get; set; }

        [Required]
        public string Breakfast { get; set; }

    }
}
