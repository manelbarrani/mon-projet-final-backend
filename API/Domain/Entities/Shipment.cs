using Domain.Common;

namespace Domain.Entities
{
    public class Shipment : Entity
    {
        public string Number { get; set; }
        public string Status { get; set; }
        public string Start_X { get; set; }
        public string Start_Y { get; set; }
        public string Destination_X { get; set; }
        public string Destination_Y { get; set; }
        public string CurrentPosition_X { get; set; }
        public string CurrentPosition_Y { get; set; }
        public string CurrentHumidity { get; set; }
        public string CurrentTemperature { get; set; }
        public string CurrentDensity { get; set; }
        public string StartAdr { get; set; }
        public string DestinationAdr { get; set; }
        public Guid NavigatorId { get; set; }
        public Navigator Navigator { get; set; }
        public Guid VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }
    }
}
