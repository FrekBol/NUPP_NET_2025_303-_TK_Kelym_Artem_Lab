namespace AviationSystem.REST.Models
{
    // Models/AircraftModel.cs
    public abstract class AircraftModel
    {
        public Guid Id { get; set; }                    // Унікальний ідентифікатор
        public string Manufacturer { get; set; }        // Виробник (Boeing, Sukhoi тощо)
        public string ModelName { get; set; }           // Назва моделі (737, Su-35 тощо)
        public int ProductionYear { get; set; }         // Рік випуску
        public double MaxSpeedKmh { get; set; }         // Максимальна швидкість, км/год
        public double RangeKm { get; set; }             // Дальність польоту, км
        public double ServiceCeilingMeters { get; set; }// Практична стеля, м
        public double EmptyWeightKg { get; set; }       // Порожня вага, кг
    }
}
