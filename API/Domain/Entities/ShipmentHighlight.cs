using Domain.Common;

namespace Domain.Entities
{
    public class ShipmentHighlight : Entity
    {
        public string CurrentPosition_X { get; set; }
        public string CurrentPosition_Y { get; set; }
        public string CurrentHumidity { get; set; }
        public string CurrentTemperature { get; set; }
        public string CurrentDensity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid ShimpentId { get; set; }

    }
}
