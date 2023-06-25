namespace backend.Data
{
    public class PagingData
    {
        public string Breakfast { get; set; }
        public string Lunch { get; set; }

        public DayOfWeek DayCreated { get; set; }
        public DateTime DateCreated { get; set; }

        public PagingData(
            string Breakfast, 
            string Lunch,
            DayOfWeek DayCreated, 
            DateTime DateCreated
            )
        {
            this.Breakfast = Breakfast;
            this.Lunch = Lunch;
            this.DayCreated = DayCreated;
            this.DateCreated = DateCreated;
        }
    }
}
