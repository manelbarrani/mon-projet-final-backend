using Application.Interfaces;
using Application.Setting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehiculeFeature.Queries
{
    public class GetListVehiculeByStatus : IRequest<ResponseHttp>
    {
        public int Status { get; set; }
        public class GetListVehiculeByStatusHandler : IRequestHandler<GetListVehiculeByStatus, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public GetListVehiculeByStatusHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }
            public Task<ResponseHttp> Handle(GetListVehiculeByStatus request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
