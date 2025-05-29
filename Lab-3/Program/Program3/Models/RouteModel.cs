using System.Collections.Generic;

namespace Program3.Models
{
    public class RouteModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ExternalId { get; set; } = Guid.NewGuid();

        public ICollection<BusModel> Buses { get; set; } = new List<BusModel>();
    }
}