using System;

namespace RentCar.Application.Models
{
    public class TokenInfo
    {
        public string? Token { get; set; }
        public DateTime FechaExp { get; set; }
    }
}