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

        //Veg, Non-Veg
        public string Breakfast { get; set; }
        
        //Veg, Non-Veg
        public string Lunch { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateCreated { get; set; }


        public DayOfWeek DayCreated { get; set; }

        
    }
}
