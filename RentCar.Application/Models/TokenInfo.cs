using System;

namespace RentCar.Application.Models
{
    public class TokenInfo
    {
        public string? Token { get; set; }
        public DateTime FechaExp { get; set; }
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string FirstName { get; set; }
    }
}