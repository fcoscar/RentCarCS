using System;

namespace RentCar.domain.Core
{
    public class BaseEntity
    {
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaMod { get; set; }
    }
}