using Application.Features.ShipmentsFeature.Dto;
using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ShipmentsFeature.Queries
{
    public class GetShipmentByIdQuery : IRequest<ResponseHttp>
    {
        public Guid Id { get; set; }

        public GetShipmentByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetShipmentByIdQueryHandler : IRequestHandler<GetShipmentByIdQuery, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public GetShipmentByIdQueryHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }
            public async Task<ResponseHttp> Handle(GetShipmentByIdQuery request, CancellationToken cancellationToken)
            {
                var shipment = await _trackingContext.Shipments
                    .Where(x => x.Id == request.Id)
                    .Select (x=> new ShipmentDto()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        StartAdr = x.StartAdr,
                        Start_X = x.Start_X,
                        Start_Y = x.Start_Y,
                        CreatedDate = x.CreatedDate,
                        Number = x.Number,
                        Destination_X =x.Destination_X,
                        DestinationAdr= x.DestinationAdr,
                        Destination_Y= x.Destination_Y,
                        NavigatorId = x.NavigatorId,
                        VehiculeId = x.VehiculeId,
                    })
                    .SingleOrDefaultAsync(cancellationToken);
                if (shipment == null)
                    return new ResponseHttp()
                    {
                        Resultat = "Not Found",
                        Status = 404,
                        Fail_Messages = "NoT Exist a shipment with this Id"
                    };
                return new ResponseHttp()
                {
                    Resultat = shipment,
                    Status = 200,
                    Fail_Messages = "None"
                };
            }
        }
    }
}
