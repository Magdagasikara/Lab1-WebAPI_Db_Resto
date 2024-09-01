using AutoMapper;
using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services.IServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace Lab1_WebAPI_Db_Resto.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly ITableRepository _tableRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IMapper _mapper;
        public BookingServices(IBookingRepository bookingRepo, ITableRepository tableRepo, ICustomerRepository customerRepo, IMapper mapper)
        {
            _bookingRepo = bookingRepo;
            _tableRepo = tableRepo;
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task AddBookingAsync(BookingDto booking)
        {
            try
            {
                var newBooking = _mapper.Map<Booking>(booking);
                newBooking.TimeStamp = DateTime.Now;
                newBooking.ReservationEnd = booking.ReservationStart.AddHours(booking.ReservationDurationInHours);
                newBooking.Customer = await _customerRepo.GetCustomerByEmailAsync(booking.Email);
                newBooking.BookingNumber = $"{newBooking.Customer.Id}0{newBooking.TimeStamp:yyyyMMdd}";
                var freeTables = await _tableRepo.BookAndGetTablesByTimeAsync(newBooking);
                // här imellan!
                await _bookingRepo.AddBookingAsync(newBooking);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteBookingByBookingNumberAsync(string bookingNr)
        {
            try
            {
                // HÄR MÅSTE JAG TA BORT FRÅN TABLEBOOKING TABELLEN OCKSÅ
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                await _bookingRepo.DeleteBookingByBookingNumberAsync(bookingNr);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting a booking in service", ex);
            }
        }

        public async Task<IEnumerable<BookingWithTablesListVM>> GetActiveBookingsAsync(DateTime dateTime)
        {
            try
            {
                var bookings = await _bookingRepo.GetActiveBookingsAsync(dateTime);

                return _mapper.Map<List<BookingWithTablesListVM>>(bookings);
                // komplettera med tables eller kommer de med ??????
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting bookings in service", ex);
            }
        }

        public async Task<IEnumerable<BookingListVM>> GetActiveBookingsByEmailAsync(DateTime dateTime, string email)
        {
            try
            {
                var bookings = await _bookingRepo.GetActiveBookingsByEmailAsync(dateTime, email);
                return _mapper.Map<List<BookingListVM>>(bookings);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting bookings in service", ex);
            }
        }

        public async Task<IEnumerable<BookingWithTablesListVM>> GetAllBookingsAsync()
        {
            try
            {
                var bookings = await _bookingRepo.GetAllBookingsAsync();
                var bookingsWithTables = new List<BookingWithTablesListVM>();
                    //_mapper.Map<List<BookingWithTablesListVM>>(bookings);
                foreach (Booking booking in bookings)
                {
                    var getBooking = _mapper.Map<BookingWithTablesListVM>(booking);
                    getBooking.Email = booking.Customer.Email;
                    getBooking.Tables = new List<TableListVM>();
                    foreach (var tableBooking in booking.TableBookings)
                    {
                        var tablevm=_mapper.Map<TableListVM>(tableBooking.Table);
                        getBooking.Tables.Add(tablevm);
                    }
                    bookingsWithTables.Add(getBooking);
                }
                return bookingsWithTables;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting bookings in service", ex);
            }
        }

        public async Task<IEnumerable<BookingListVM>> GetAllBookingsByEmailAsync(string email)
        {
            try
            {
                var bookings = await _bookingRepo.GetAllBookingsByEmailAsync(email);

                return _mapper.Map<List<BookingListVM>>(bookings);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting bookings in service", ex);
            }
        }

        public async Task<BookingListVM> GetBookingByBookingNumberAsync(string bookingNr)
        {
            try
            {
                var booking = await _bookingRepo.GetBookingByBookingNumberAsync(bookingNr);
                return _mapper.Map<BookingListVM>(booking);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting booking in service", ex);
            }
        }

        public async Task<IEnumerable<BookingWithTablesListVM>> GetBookingsByDateAsync(DateOnly date)
        {
            try
            {
                var bookings = await _bookingRepo.GetBookingsByDateAsync(date);
                return _mapper.Map<List<BookingWithTablesListVM>>(bookings);
                // komplettera med tables eller kommer de med ??????
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting bookings in service", ex);
            }
        }

        public async Task<BookingWithTablesListVM> GetBookingWithTablesByBookingNumberAsync(string bookingNr)
        {
            try
            {
                var booking = await _bookingRepo.GetBookingWithTablesByBookingNumberAsync(bookingNr);
                return _mapper.Map<BookingWithTablesListVM>(booking);
                // komplettera med tables kanske behövs det!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting booking in service", ex);
            }
        }

        public async Task UpdateBookingAsync(string bookingNr, BookingDto updatedBooking)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookingTablesAsync(string bookingNr, BookingWithTablesDto updatedBookingWithTables)
        {
            throw new NotImplementedException();
        }
    }
}
