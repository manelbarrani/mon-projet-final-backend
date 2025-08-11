using Application.Interfaces;
using Application.Setting;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductFeature.Queries
{
    public class GetListProductQuery : IRequest<ResponseHttp>
    {
        public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, ResponseHttp>
        {
            private readonly IDematContext _dematContext;

            public GetListProductQueryHandler(IDematContext dematContext)
            {
                _dematContext = dematContext;
            }

            public async Task<ResponseHttp> Handle(GetListProductQuery request, CancellationToken cancellationToken)
            {
                var products = await _dematContext.Products
                    .Where(x => x.IsDeleted == false)
                    .ToListAsync(cancellationToken);

                return new ResponseHttp
                {
                    Status = 200,
                    Fail_Messages = "None",
                    Resultat = new
                    {
                        Products = products
                    }
                };
            }
        }
    }
}
