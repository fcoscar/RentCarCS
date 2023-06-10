namespace RentCar.Application.Models
{
    public class CarGetModel
    {
        public int Id { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Year { get; set; }
        public int Pasajeros { get; set; }
        public string? Descripcion { get; set; }
        public decimal PricePerDay { get; set; }
        public string? Categoria { get; set; }
        public int CategoriaId { get; set; }
    }
}