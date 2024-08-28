namespace Lab1_WebAPI_Db_Resto.Models
{
    public class Table
    {
        public int Id{ get; set; }
        public int TableNumber { get; set; }
        public int AmountOfPlaces { get; set; }

        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
