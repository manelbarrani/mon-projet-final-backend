using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VehiculeFeature.Queries
{
    public class GetVehiculeByIdQuery : IRequest<ResponseHttp>
    {
        public Guid Id { get; set; }

        public GetVehiculeByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetShipmentByIdQueryHandler : IRequestHandler<GetVehiculeByIdQuery, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public GetShipmentByIdQueryHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }
            public async Task<ResponseHttp> Handle(GetVehiculeByIdQuery request, CancellationToken cancellationToken)
            {
                var shipment = await _trackingContext.Shipments
                    .Where(x => x.Id == request.Id)
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
