namespace AviationSystem.REST.Models
{
    // Models/MilitaryAircraftModel.cs
    public class MilitaryAircraftModel : AircraftModel
    {
        public double MaxGForce { get; set; }           // Максимальне допустиме навантаження G
        public bool HasStealthCapability { get; set; }  // Стелс-технології
        public int NumberOfHardpoints { get; set; }     // Кількість пілонів для озброєння
        public IEnumerable<string> StandardArmaments { get; set; }
        // Перелік штатних озброєнь (ракети, бомби)
        public string RadarType { get; set; }           // Тип РЛС
    }
}
