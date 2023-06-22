using System.ComponentModel.DataAnnotations;

namespace backend.Models.Dto
{
    public class OrderDto
    {
        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string OrderType { get; set; }

        [Required]
        public string FoodType { get; set; }
    }
}
