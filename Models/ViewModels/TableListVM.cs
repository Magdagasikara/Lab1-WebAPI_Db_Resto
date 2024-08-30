using System.ComponentModel.DataAnnotations;

namespace Lab1_WebAPI_Db_Resto.Models.ViewModels
{
    public class TableListVM
    {
        public int TableNumber { get; set; }
        public int AmountOfPlaces { get; set; }
    }
}
