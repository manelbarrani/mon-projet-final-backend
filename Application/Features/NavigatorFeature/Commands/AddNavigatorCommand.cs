using Application.Interfaces;
using Application.Setting;
using Domain.Entities;
using MediatR;

namespace Application.Features.NavigatorFeature.Commands
{
    public class AddNavigatorCommand : IRequest<ResponseHttp>
    {
        public AddNavigatorCommand(string name, string companyName, string contact)
        {
            Name = name;
            CompanyName = companyName;
            Contact = contact;
        }

        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Contact { get; set; }

        public class AddNavigatorCommandHandler : IRequestHandler<AddNavigatorCommand, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public AddNavigatorCommandHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }

            public async Task<ResponseHttp> Handle(AddNavigatorCommand request, CancellationToken cancellationToken)
            {
                var Navigator = new Navigator()
                {
                    Name = request.Name,
                    CompanyName = request.CompanyName,
                    Contact = request.Contact,
                };
                _trackingContext.Navigators.Add(Navigator);
                await _trackingContext.SaveChangesAsync(cancellationToken);
                return new ResponseHttp()
                {
                    Resultat = new
                    {
                        Navigator.Id
                    },
                    Status = 200,
                    Fail_Messages = "None"
                };
            }
        }
    }
}
