using System;

namespace RentCar.domain.Core
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            FechaMod = DateTime.Now;
        }
        public int? IdUsuarioCreacion { get; set; }
        public DateTime? FechaMod { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}