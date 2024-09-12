using Lab1_WebAPI_Db_Resto.Models.DTOs.Table;
using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.ViewModels
{
    public class BookingWithTablesListVM
    {
        public string BookingNumber { get; set; }
        public string Email { get; set; }
        public DateTime TimeStamp { get; set; }
        public int AmountOfGuests { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }

        public ICollection<TableDto>? Tables { get; set; }

    }
}
