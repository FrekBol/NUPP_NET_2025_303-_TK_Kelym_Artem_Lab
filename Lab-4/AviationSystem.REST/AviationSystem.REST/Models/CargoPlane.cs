namespace AviationSystem.REST.Models
{
    // Models/CargoAircraftModel.cs
    public class CargoAircraftModel : AircraftModel
    {
        public double MaxPayloadKg { get; set; }        // Максимальне корисне навантаження, кг
        public double CargoVolumeCbm { get; set; }      // Корисний об’єм вантажного відсіку, м³
        public bool HasRollOnRollOffCapability { get; set; }
        // Можливість навантаження RORO
        public int NumberOfCargoDoors { get; set; }     // Кількість вантажних люків
        public bool HasTemperatureControl { get; set; } // Система контролю температури вантажного відсіку
    }
}
