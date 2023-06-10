namespace RentCar.Application.Core
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            Succes = true;
        }

        public string Message { get; set; }
        public bool Succes { get; set; }
        public dynamic Data { get; set; }
    }
}