using Application.Interfaces;
using Application.Setting;
using Domain.Entities;
using MediatR;

namespace Application.Features.ShipmentsFeature.Commands
{
    public class AddShipmentCommand : IRequest<ResponseHttp>
    {
        public AddShipmentCommand(string name, string companyName, string contact)
        {
            Name = name;
            CompanyName = companyName;
            Contact = contact;
        }

        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Contact { get; set; }

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
