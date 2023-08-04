using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VehiculeFeature.Queries
{
    public class GetListVehiculeQuery : IRequest<ResponseHttp>
    {
        public class GetListvehiculeQueryHandler : IRequestHandler<GetListVehiculeQuery, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public GetListvehiculeQueryHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }
            public async Task<ResponseHttp> Handle(GetListVehiculeQuery request, CancellationToken cancellationToken)
            {
                var vehicules = await _trackingContext.Vehicules.Where(x => x.IsDeleted == false).ToListAsync(cancellationToken);
                return new ResponseHttp
                {
                    Status = 200,
                    Fail_Messages = "None",
                    Resultat = new
                    {
                        vehicules= vehicules
                    }
                };
            }
        }
    }
}
