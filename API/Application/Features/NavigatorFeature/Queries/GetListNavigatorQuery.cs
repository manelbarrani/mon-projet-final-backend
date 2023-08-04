using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.NavigatorFeature.Queries
{
    public class GetListNavigatorQuery : IRequest<ResponseHttp>
    {
        public class GetListNavigatorQueryHandler : IRequestHandler<GetListNavigatorQuery, ResponseHttp>
        {
            private readonly ITrackingContext _trackingContext;

            public GetListNavigatorQueryHandler(ITrackingContext trackingContext)
            {
                _trackingContext = trackingContext;
            }
            public async Task<ResponseHttp> Handle(GetListNavigatorQuery request, CancellationToken cancellationToken)
            {
                var navigators = await _trackingContext.Navigators.Where(x=>x.IsDeleted==false).ToListAsync(cancellationToken);
                return new ResponseHttp
                {
                    Status = 200,
                    Fail_Messages = "None",
                    Resultat = new
                    {
                        Navigators = navigators
                    }
                };
            }
        }
    }
}
