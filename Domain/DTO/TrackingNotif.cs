using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class TrackingNotif
    {
        public Guid shipment_id { get; set; }
        public double temperature { get; set; }
        public double humidity { get; set; }
        public double luminosity { get; set; }
        public double gaz_quality { get; set; }
        public double intrusion_control { get; set; }
        public double accelerometer { get; set; }
        public double gyro { get; set; }
        public double magnetometer { get; set; }
        public bool choc_detector { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
