using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShipmentsFeature.Dto
{
    public class ShimpentDetailsDto
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
