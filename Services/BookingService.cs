﻿using AutoMapper;
using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs.Booking;
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
        private readonly IMapper _mapper;
        public BookingService(IBookingRepository bookingRepo, ITableRepository tableRepo, ICustomerRepository customerRepo, IMapper mapper)
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
                newBooking.BookingNumber = $"{newBooking.Customer.Id}0{newBooking.TimeStamp:yyyyMMddhhmmss}";
                var freeTables = await _tableRepo.BookAndGetTablesByTimeAsync(newBooking);
                // new Booking is created automatically when TableBooking is created
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

        public async Task<IEnumerable<BookingWithTablesEndTimeDto>> GetActiveBookingsAsync(DateTime dateTime)
        {
            try
            {
                var bookings = await _bookingRepo.GetActiveBookingsAsync(dateTime);

                return _mapper.Map<List<BookingWithTablesEndTimeDto>>(bookings);
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
                        var tablevm=_mapper.Map<TableDto>(tableBooking.Table);
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

        public async Task<IEnumerable<BookingWithTablesEndTimeDto>> GetBookingsByDateAsync(DateOnly date)
        {
            try
            {
                var bookings = await _bookingRepo.GetBookingsByDateAsync(date);
                return _mapper.Map<List<BookingWithTablesEndTimeDto>>(bookings);
                // komplettera med tables eller kommer de med ??????
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting bookings in service", ex);
            }
        }

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
