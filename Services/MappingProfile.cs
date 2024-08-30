using AutoMapper;
using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Models;

namespace Lab1_WebAPI_Db_Resto.Services
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Mapping mellan modeller och DTOs/ViewModels
            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();
            CreateMap<Customer, CustomerListVM>();

            CreateMap<TableDto, Table>();
            CreateMap<TableUpdateDto, Table>();
            CreateMap<Table, TableListVM>();

            CreateMap<BookingDto, Booking>();
            CreateMap<Booking, BookingListVM>();
            CreateMap<Booking, BookingWithTablesListVM>();
        }
    }
}
