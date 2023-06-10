namespace RentCar.Infraestructure.Models
{
    public class UserCarModel
    {
        public UserCarModel()
        {
            this.CarModel = new CarModel();
            this.UserModel = new UserModel();
        }
        public UserModel UserModel { get; set; }
        public CarModel CarModel { get; set; }
    }
}