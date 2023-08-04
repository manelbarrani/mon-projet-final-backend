using Application.Interfaces;
using Application.Setting;
using Domain.Entities;
using MediatR;

namespace Application.Features.VehiculeFeature.Commands
{
    public class AddVehiculeCommand : IRequest<ResponseHttp>
    {
        public AddVehiculeCommand(string name, string companyName, string contact)
        {
            Name = name;
            CompanyName = companyName;
            Contact = contact;
        }

        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Contact { get; set; }

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
