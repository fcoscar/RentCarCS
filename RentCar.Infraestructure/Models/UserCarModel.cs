namespace RentCar.Infraestructure.Models
{
    public class UserCarModel
    {
        public UserCarModel()
        {
            CarModel = new CarModel();
            UserModel = new UserModel();
        }

        public UserModel UserModel { get; set; }
        public CarModel CarModel { get; set; }
    }
}