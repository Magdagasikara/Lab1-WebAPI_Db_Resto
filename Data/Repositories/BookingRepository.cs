﻿using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Lab1_WebAPI_Db_Resto.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly RestoContext _context;
        public BookingRepository(RestoContext context)
        {
            _context = context;
        }
        public async Task AddBookingAsync(Booking booking)
        {
            try
            {
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error when adding a new table", ex);
            }
        }

        public async Task DeleteBookingByBookingNumberAsync(int bookingNr)
        {
            try
            {
                var booking = await GetBookingByBookingNumberAsync(bookingNr);
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when deleting a booking", ex);
            }
        }

        public async Task<IEnumerable<Booking>> GetActiveBookingsAsync(DateTime dateTime)
        {
            try
            {
                return await _context.Bookings
                    .Where(b => b.ReservationEnd > dateTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting a list of bookings", ex);
            }
        }

        public async Task<IEnumerable<Booking>> GetActiveBookingsByEmailAsync(DateTime dateTime, string email)
        {
            try
            {
                return await _context.Bookings
                    .Where(b => b.ReservationEnd > dateTime && b.Customer.Email == email)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting a list of bookings", ex);
            }
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsByEmailAsync(string email)
        {
            try
            {
                return await _context.Bookings
                    .Where(b => b.Customer.Email == email)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting a list of bookings", ex);
            }
        }

        public async Task<Booking> GetBookingByBookingNumberAsync(int bookingNr)
        {
            try
            {
                var booking = await _context.Bookings
                    .SingleOrDefaultAsync(c => c.BookingNumber == bookingNr);
                if (booking != null)
                {
                    return booking;
                }
                throw new KeyNotFoundException($"Booking number not found.");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when getting a booking", ex);
            }
        }


        public async Task<IEnumerable<Booking>> GetBookingsByDateAsync(DateOnly date)
        {
            var startDateTime = date.ToDateTime(TimeOnly.MinValue);
            var endDateTime = date.ToDateTime(TimeOnly.MaxValue);
            try
            {
                return await _context.Bookings
                    .Where(b => b.ReservationEnd.Date == startDateTime || b.ReservationEnd.Date == endDateTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting a list of bookings", ex);
            }
        }

        public async Task<Booking> GetBookingWithTablesByBookingNumberAsync(int bookingNr)
        {
            try
            {
                var booking = await _context.Bookings
                    .Include(b => b.TableBookings)
                    .SingleOrDefaultAsync(c => c.BookingNumber == bookingNr);
                if (booking != null)
                {
                    return booking;
                }
                throw new KeyNotFoundException($"Booking number not found.");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when getting a booking", ex);
            }
        }

        public async Task UpdateBookingAsync(int bookingNr, Booking updatedBooking)
        {
            if (updatedBooking == null || bookingNr == 0)
            {
                throw new ArgumentNullException(nameof(updatedBooking), "No booking to be updated");
            }
            try
            {
                var booking = await GetBookingByBookingNumberAsync(bookingNr);
                booking.ReservationStart = updatedBooking.ReservationStart;
                booking.ReservationEnd = updatedBooking.ReservationEnd;
                booking.AmountOfGuests = updatedBooking.AmountOfGuests;
                booking.TimeStamp = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Booking to be updated does not exist in Db");
            }
            catch (Exception ex)
            {
                throw new Exception("Error when updating a booking", ex);
            }
        }

        public async Task UpdateBookingTablesAsync(int bookingNr, Booking updatedBooking, List<Models.Table> tables)
        {
            if (updatedBooking == null || bookingNr == 0)
            {
                throw new ArgumentNullException(nameof(updatedBooking), "No booking to be updated");
            }
            try
            {
                var booking = await GetBookingByBookingNumberAsync(bookingNr);
                booking.ReservationStart = updatedBooking.ReservationStart;
                booking.ReservationEnd = updatedBooking.ReservationEnd;
                booking.AmountOfGuests = updatedBooking.AmountOfGuests;
                booking.TimeStamp = DateTime.Now;
                // byt TABLES HÄR
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // mer hantering för bord!!!!!!!!!!!!!!!!!
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Booking to be updated does not exist in Db");
            }
            catch (Exception ex)
            {
                throw new Exception("Error when updating a booking", ex);
            }
        }
    }
}
