using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("user")] 
        public string UserEmail { get; set; }

        public User user { get; set; }

        // breakfast, lunch, dinner 
        [Required]
        public string OrderType { get; set; }

        // veg, Nonveg
        [Required]
        public string FoodType { get; set; } 
    }
}
