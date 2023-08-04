using Application.Interfaces;
using Application.Setting;
using Domain.Entities;
using MediatR;

namespace Application.Features.ShipmentsFeature.Commands
{
    public class AddShipmentCommand : IRequest<ResponseHttp>
    {

        public string Number { get; set; }
        public string Start_X { get; set; }
        public string Start_Y { get; set; }
        public string Destination_X { get; set; }
        public string Destination_Y { get; set; }
        public string StartAdr { get; set; }
        public string DestinationAdr { get; set; }
        public Guid NavigatorId { get; set; }
        public Guid VehiculeId { get; set; }

        public class AddShipmentCommandHandler : IRequestHandler<AddShipmentCommand, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public AddShipmentCommandHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }

            public async Task<ResponseHttp> Handle(AddShipmentCommand request, CancellationToken cancellationToken)
            {
                var shipment = new Shipment()
                {
                    StartAdr = request.StartAdr,
                    Start_X= request.Start_X,
                    Start_Y= request.Start_Y,
                    Status="1",
                    DestinationAdr = request.DestinationAdr,
                    Destination_X = request.Destination_X,
                    Destination_Y= request.Destination_Y,
                    NavigatorId = request.NavigatorId,
                    VehiculeId = request.VehiculeId,
                    Number =request.Number
                    
                };
                _trackingContext.Shipments.Add(shipment);
                await _trackingContext.SaveChangesAsync(cancellationToken);
                return new ResponseHttp()
                {
                    Resultat = new
                    {
                       Id= shipment.Id
                    },
                    Status = 200,
                    Fail_Messages = "None"
                };
            }
        }
    }
}
