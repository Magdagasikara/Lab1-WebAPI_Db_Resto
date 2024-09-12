using AutoMapper;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Booking;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Customer;
using Lab1_WebAPI_Db_Resto.Models.DTOs.MealCategory;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Meal;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Table;

namespace Lab1_WebAPI_Db_Resto.Services
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Mapping mellan modeller och DTOs/ViewModels
            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();
            CreateMap<Customer, CustomerDto>();

            CreateMap<TableDto, Table>();
            CreateMap<TableUpdateDto, Table>();
            CreateMap<Table, TableDto>();

            CreateMap<BookingDto, Booking>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Booking, BookingListVM>();
            CreateMap<Booking, BookingWithTablesListVM>();

            CreateMap<MealDto, Meal>();
            CreateMap<MealWithCategoryDto, Meal>();
            CreateMap<MealUpdateDto, Meal>();
            CreateMap<Meal, MealWithCategoryDto>();
            CreateMap<Meal, MealDto>();

            CreateMap<MealCategoryDto, MealCategory>(); 
            CreateMap<MealCategoryWithMealsDto, MealCategory>();
            CreateMap<MealCategoryUpdateDto, MealCategory>();
            CreateMap<MealCategory, MealCategoryDto>();
            CreateMap<MealCategory, MealCategoryWithMealsDto>();
        }
    }
}
