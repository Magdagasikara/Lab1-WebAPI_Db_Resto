using AutoMapper;
using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Booking;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Customer;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Table;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;
using Lab1_WebAPI_Db_Resto.Services.IServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace Lab1_WebAPI_Db_Resto.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly ITableRepository _tableRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingService> _logger;
        public BookingService(IBookingRepository bookingRepo, ITableRepository tableRepo, ICustomerRepository customerRepo, ICustomerService customerService, IMapper mapper, ILogger<BookingService> logger)
        {
            _bookingRepo = bookingRepo;
            _tableRepo = tableRepo;
            _customerRepo = customerRepo;
            _customerService = customerService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddBookingAsync(BookingDto booking)
        {
            try
            {
                var newBooking = _mapper.Map<Booking>(booking);
                newBooking.TimeStamp = DateTime.Now;
                newBooking.ReservationEnd = booking.ReservationStart.AddHours(booking.ReservationDurationInHours);
                newBooking.Customer = await _customerRepo.GetCustomerByEmailAsync(booking.Email);
                newBooking.BookingNumber = $"{newBooking.Customer.Id}0{newBooking.TimeStamp:yyyyMMddhhmmss}";
                var bookedTables = await _tableRepo.AssignTablesToBookingAsync(newBooking);
                await _tableRepo.BookTablesAsync(newBooking, bookedTables);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteBookingByBookingNumberAsync(string bookingNr)
        {
            try
            {
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

        //public async Task<IEnumerable<BookingWithTablesEndTimeDto>> GetActiveBookingsAsync(DateTime dateTime)
        //{
            
        //}

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

        public async Task<IEnumerable<BookingWithTablesEndTimeDto>> GetAllBookingsAsync()
        {
            try
            {
                var bookings = await _bookingRepo.GetAllBookingsAsync();
                var bookingsWithTables = new List<BookingWithTablesEndTimeDto>();
                foreach (Booking booking in bookings)
                {
                    var getBooking = _mapper.Map<BookingWithTablesEndTimeDto>(booking);
                    getBooking.Email = booking.Customer.Email;
                    getBooking.Tables = new List<TableDto>();
                    foreach (var tableBooking in booking.TableBookings)
                    {
                        var tablevm = _mapper.Map<TableDto>(tableBooking.Table);
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
                var getBooking = _mapper.Map<BookingListVM>(booking);
                getBooking.Email = booking.Customer.Email;
                return getBooking;
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

        //public async Task<IEnumerable<BookingWithTablesEndTimeDto>> GetBookingsByDateAsync(DateOnly date)
        //{
         
        //}

        public async Task<BookingWithTablesEndTimeDto> GetBookingWithTablesByBookingNumberAsync(string bookingNr)
        {
            try
            {
                var booking = await _bookingRepo.GetBookingByBookingNumberAsync(bookingNr);
                var getBooking = _mapper.Map<BookingWithTablesEndTimeDto>(booking);
                getBooking.Email = booking.Customer.Email;
                getBooking.Tables = new List<TableDto>();
                foreach (var tableBooking in booking.TableBookings)
                {
                    var tablevm = _mapper.Map<TableDto>(tableBooking.Table);
                    getBooking.Tables.Add(tablevm);
                }
                return getBooking;
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

        public async Task UpdateBookingAsync(BookingUpdateDto booking)
        {

            try
            {
                // Gathering info on the old bookingOriginal and new bookingToUpdate
                var bookingNumber = booking.BookingNumber;

                _logger.LogInformation($"Getting original info on the booking to update at time {DateTime.Now}.");
                var bookingOriginal = await _bookingRepo.GetBookingByBookingNumberAsync(bookingNumber);
                _logger.LogInformation($"Original booking fetched at time {DateTime.Now}.");

                var bookingToUpdate = _mapper.Map<Booking>(booking);
                bookingToUpdate.ReservationEnd = booking.ReservationStart.AddHours(booking.ReservationDurationInHours);

                bookingToUpdate.TableBookings = bookingOriginal.TableBookings;
                bookingToUpdate.Customer = bookingOriginal.Customer;

                var possibleSeatsAtOriginalBooking = bookingOriginal
                    .TableBookings
                    .Sum(t => t.Table.AmountOfPlaces);
                Console.WriteLine($"possible seats={possibleSeatsAtOriginalBooking}, booked={bookingOriginal.AmountOfGuests}");


                // Case 1: email has changed
                if (booking.Email != bookingOriginal.Customer.Email)
                {
                    _logger.LogInformation($"Checking if new email encountered for the booking is registered in the db.");
                    var customer = await _customerRepo.GetCustomerByEmailAsync(booking.Email);

                    // email changed and is not found in db - update customers email
                    if (customer is null)
                    {
                        _logger.LogInformation($"New email is not found in db and will be updated for the customer.");
                        customer = await _customerRepo.GetCustomerByEmailAsync(bookingOriginal.Customer.Email);
                        var customerToUpdate = _mapper.Map<CustomerUpdateDto>(customer);
                        customerToUpdate.UpdatedEmail = booking.Email;
                        await _customerService.UpdateCustomerAsync(customerToUpdate);
                        customer = await _customerRepo.GetCustomerByEmailAsync(booking.Email);
                    }
                    else
                    {
                        _logger.LogInformation($"New email is found in db and booking will be assigned to the found customer.");
                    }
                    bookingToUpdate.Customer = customer;
                    _logger.LogInformation($"Booking update with new email will be done at time {DateTime.Now}.");
                }

                // Case 2: reservation time, duration changed or amount of guests changed 
                if (bookingToUpdate.AmountOfGuests != bookingOriginal.AmountOfGuests || bookingToUpdate.ReservationStart != bookingOriginal.ReservationStart || bookingToUpdate.ReservationEnd != bookingOriginal.ReservationEnd)
                {
                    // HÄRIFRÅN URKLIPPT
                    var bookedTables = await _tableRepo.AssignTablesToBookingAsync(bookingToUpdate, bookingOriginal);
                    foreach (var table in bookedTables)
                    {
                        Console.WriteLine($"table number {table.TableNumber}");
                    }

                    await _tableRepo.UpdateTablesAsync(bookingToUpdate, bookedTables);
                }

                // Update the booking
                await _bookingRepo.UpdateBookingAsync(bookingToUpdate);
                _logger.LogInformation($"Booking updated at time {DateTime.Now}.");


            }

            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating customer in service", ex);
            }

        }

        public Task UpdateBookingTablesAsync(string bookingNr, BookingWithTablesDto updatedBookingWithTables)
        {
            // should only be possible for admin
            throw new NotImplementedException();
        }
    }
}
