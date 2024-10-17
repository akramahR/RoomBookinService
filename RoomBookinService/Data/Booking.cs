namespace RoomBookinService.Data
{
    public class Booking
    {
        public int Id { get; set; }

        // Foreign keys
        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        // Booking details
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

    }
}
