using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShipmentsFeature.Dto
{
    public class ShipmentDto
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public string Start_X { get; set; }
        public string Start_Y { get; set; }
        public string Destination_X { get; set; }
        public string Destination_Y { get; set; }
        public string StartAdr { get; set; }
        public string DestinationAdr { get; set; }
        public Guid NavigatorId { get; set; }
        public Guid VehiculeId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
