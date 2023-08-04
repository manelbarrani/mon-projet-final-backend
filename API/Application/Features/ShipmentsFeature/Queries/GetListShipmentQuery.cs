using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ShipmentsFeature.Queries
{
    public class GetListShipmentQuery : IRequest<ResponseHttp>
    {
        public class GetListShipmentQueryHandler : IRequestHandler<GetListShipmentQuery, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public GetListShipmentQueryHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }
            public async Task<ResponseHttp> Handle(GetListShipmentQuery request, CancellationToken cancellationToken)
            {
                var shipments = await _trackingContext.Shipments.Where(x => x.IsDeleted == false).ToListAsync(cancellationToken);
                return new ResponseHttp
                {
                    Status = 200,
                    Fail_Messages = "None",
                    Resultat = new
                    {
                        shipments = shipments
                    }
                };
            }
        }
    }
}
