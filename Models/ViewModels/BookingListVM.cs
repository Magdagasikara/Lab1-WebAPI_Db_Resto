using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.ViewModels
{
    public class BookingListVM
    {
        public int BookingNumber { get; set; }
        public string Email { get; set; }
        public DateTime TimeStamp { get; set; }
        public int AmountOfGuests { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }

    }
}
