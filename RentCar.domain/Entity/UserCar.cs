namespace RentCar.domain.Entity
{
    public class UserCar
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
        public virtual Car? Car { get; set; }
    }
}