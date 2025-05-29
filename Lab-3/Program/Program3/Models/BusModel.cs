namespace Program3.Models
{
    public class BusModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public Guid ExternalId { get; set; } = Guid.NewGuid();

        public int? RouteModelId { get; set; }
        public RouteModel Route { get; set; }

        public DriverModel Driver { get; set; }
    }
}