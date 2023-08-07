using Application.Interfaces;
using Application.Setting;
using Domain.Entities;
using MediatR;

namespace Application.Features.VehiculeFeature.Commands
{
    public class AddVehiculeCommand : IRequest<ResponseHttp>
    {
        public AddVehiculeCommand(string vRN, string vIN)
        {
            VRN = vRN;
            VIN = vIN;
        }

        public string VRN { get; set; }
        public string VIN { get; set; }

        public class AddVehiculeCommandHandler : IRequestHandler<AddVehiculeCommand, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public AddVehiculeCommandHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }

            public async Task<ResponseHttp> Handle(AddVehiculeCommand request, CancellationToken cancellationToken)
            {
                var vehicule = new Vehicule()
                {
                    VIN = request.VIN,
                    VRN = request.VRN
                };
                _trackingContext.Vehicules.Add(vehicule);
                await _trackingContext.SaveChangesAsync(cancellationToken);
                return new ResponseHttp()
                {
                    Resultat = new
                    {
                       Id= vehicule.Id
                    },
                    Status = 200,
                    Fail_Messages = "None"
                };
            }
        }
    }
}
