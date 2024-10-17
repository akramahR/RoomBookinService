using Microsoft.AspNetCore.Mvc;
using RoomBookinService.Data;
using RoomBookinService.Models;
using System;
using System.Linq;

namespace RoomBookinService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Book a room
        [HttpPost("book")]
        public IActionResult BookRoom([FromBody] RoomBookingRequest bookingRequest)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Id == bookingRequest.RoomId);
            if (room == null)
                return NotFound("Room not found.");

            // Check if the room is available during the requested dates
            var isOverlappingBooking = _context.Bookings
                .Any(b => b.RoomId == bookingRequest.RoomId &&
                          bookingRequest.CheckInDate < b.CheckOutDate && bookingRequest.CheckOutDate > b.CheckInDate);

            if (isOverlappingBooking)
                return BadRequest("Room is already booked for the selected dates.");

            var guest = _context.Guests.FirstOrDefault(g => g.Email == bookingRequest.GuestEmail);
            if (guest == null)
            {
                guest = new Guest
                {
                    Name = bookingRequest.GuestName,
                    Email = bookingRequest.GuestEmail,
                    PhoneNumber = bookingRequest.GuestPhoneNumber
                };
                _context.Guests.Add(guest);
            }

            var booking = new Booking
            {
                Guest = guest,
                Room = room,
                CheckInDate = bookingRequest.CheckInDate,
                CheckOutDate = bookingRequest.CheckOutDate
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return Ok("Room booked successfully for guest.");
        }

        // 2. Get room types
        [HttpGet("room-types")]
        public IActionResult GetRoomTypes()
        {
            var roomTypes = _context.Rooms.Select(r => r.Type).Distinct().ToList();
            return Ok(roomTypes);
        }

        // 3. Get specific room by ID along with booked dates
        [HttpGet("room/{roomId}")]
        public IActionResult GetRoom(int roomId)
        {
            var room = _context.Rooms.Find(roomId);
            if (room == null)
                return NotFound("Room not found.");

            // Fetch all the active bookings for this room, including guest details
            var bookedDates = _context.Bookings
                .Where(b => b.RoomId == roomId)
                .Select(b => new
                {
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    Guest = new
                    {
                        b.Guest.Id,
                        b.Guest.Name,
                        b.Guest.Email,
                        b.Guest.PhoneNumber
                    }
                })
                .ToList();

            // Fetch current or future active booking details if the room is booked
            var currentBooking = bookedDates.FirstOrDefault(b => b.CheckOutDate > DateTime.UtcNow);

            // Create a response object with room and booking details
            var roomDetails = new
            {
                RoomId = room.Id,
                Type = room.Type,
                Price = room.Price,
                AllBookings = bookedDates,
                NextBooking = currentBooking // null if no active current/future booking
            };

            return Ok(roomDetails);
        }

    }
}
