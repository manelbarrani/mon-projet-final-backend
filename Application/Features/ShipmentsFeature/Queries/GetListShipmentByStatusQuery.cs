using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShipmentsFeature.Queries
{
    public class GetListShipmentByStatusQuery : IRequest<ResponseHttp>
    {
        public int Status { get; set; }

        public GetListShipmentByStatusQuery(int status)
        {
            Status = status;
        }

        public class GetListShipmentByStatusQueryHandler : IRequestHandler<GetListShipmentByStatusQuery, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public GetListShipmentByStatusQueryHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }
            public async Task<ResponseHttp> Handle(GetListShipmentByStatusQuery request, CancellationToken cancellationToken)
            {
                var shipment = await _trackingContext.Shipments
                    .Where(x => x.Status == request.Status.ToString())
                    .ToListAsync(cancellationToken);
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
