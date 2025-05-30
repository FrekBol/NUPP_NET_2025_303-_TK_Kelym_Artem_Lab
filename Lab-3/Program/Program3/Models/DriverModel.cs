namespace Program3.Models
{
    public class DriverModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ExternalId { get; set; } = Guid.NewGuid();

        public int BusModelId { get; set; }
        public BusModel Bus { get; set; }
    }
}