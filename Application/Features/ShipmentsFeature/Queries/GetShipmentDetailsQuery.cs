using Application.Features.ShipmentsFeature.Dto;
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
    public class GetShipmentDetailsQuery : IRequest<ResponseHttp>
    {
        public GetShipmentDetailsQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public class GetShipmentDetailsQueryHandler : IRequestHandler<GetShipmentDetailsQuery, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public GetShipmentDetailsQueryHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }
            public async Task<ResponseHttp> Handle(GetShipmentDetailsQuery request, CancellationToken cancellationToken)
            {
                var shimpent = await _trackingContext.ShipmentHighlights
                    .Where(x => x.ShimpentId == request.Id)
                    .Select(x=>new ShimpentDetailsDto()
                    {
                        ShimpentId = x.ShimpentId,
                        CurrentDensity = x.CurrentDensity,
                        CurrentHumidity = x.CurrentHumidity,
                        CurrentTemperature = x.CurrentTemperature,
                        CurrentPosition_X = x.CurrentPosition_X,
                        CurrentPosition_Y = x.CurrentPosition_Y,
                        CreatedDate = x.CreatedDate,
                        
                    })
                    .OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefaultAsync(cancellationToken);
                if(shimpent == null)
                    return new ResponseHttp()
                    {
                        Status = 404,
                        Fail_Messages = "Not Found",
                        Resultat = null
                    };
                return new ResponseHttp()
                {
                    Status = 200,
                    Fail_Messages = "None",
                    Resultat = shimpent
                };
            }
        }
    }
}
