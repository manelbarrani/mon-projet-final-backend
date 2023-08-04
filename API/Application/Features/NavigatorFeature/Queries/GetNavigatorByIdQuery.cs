using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.NavigatorFeature.Queries
{
    public class GetNavigatorByIdQuery : IRequest<ResponseHttp>
    {
        public Guid Id { get; set; }

        public GetNavigatorByIdQuery(Guid id)
        {
            Id = id;
        }

        public class GetNavigatorByIdQueryHandler : IRequestHandler<GetNavigatorByIdQuery, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public GetNavigatorByIdQueryHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }
            public async Task<ResponseHttp> Handle(GetNavigatorByIdQuery request, CancellationToken cancellationToken)
            {
                var naviagtor = await _trackingContext.Navigators
                    .Where(x => x.Id == request.Id)
                    .SingleOrDefaultAsync(cancellationToken);
                if (naviagtor == null)
                    return new ResponseHttp()
                    {
                        Resultat = "Not Found",
                        Status = 404,
                        Fail_Messages = "NoT Exist a Navigator with this Id"
                    };
                return new ResponseHttp()
                {
                    Resultat = naviagtor,
                    Status = 200,
                    Fail_Messages = "None"
                };
            }
        }
    }
}
