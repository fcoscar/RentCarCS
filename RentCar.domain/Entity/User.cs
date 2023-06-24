using System;
using System.Collections.Generic;
using RentCar.domain.Core;

namespace RentCar.domain.Entity
{
    public class User 
    {
        public User()
        {
            FechaCreacion = DateTime.Now;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string? DocType { get; set; }
        public string? NumDoc { get; set; }

        public bool IsAdmin { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaMod { get; set; }
        //public ICollection<UserCar> UserCar { get; set; }
    }
}