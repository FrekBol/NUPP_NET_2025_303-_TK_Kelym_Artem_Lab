namespace AviationSystem.REST.Models
{
    // Models/CivilAircraftModel.cs
    public class CivilAircraftModel : AircraftModel
    {
        public int PassengerCapacity { get; set; }      // Пасажиромісткість
        public int NumberOfCabinClasses { get; set; }   // Кількість класів обслуговування (Economy, Business тощо)
        public bool HasInFlightEntertainment { get; set; }
        // Система розваг на борту
        public bool HasWiFi { get; set; }               // Наявність бортового Wi-Fi
        public double BaggageVolumeCbm { get; set; }    // Об’єм багажного відсіку, м³
    }
}
