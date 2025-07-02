using JwtAuth.Core.Entity;
using JwtAuth.Core.Response;
using MediatR;

namespace JwtAuth.Application.Query
{
    public class GetUserProfileQuery : IRequest<BaseResponse<User>>
    {
        public Guid Id { get; set; }

        public GetUserProfileQuery(Guid id)
        {
            Id = id;
        }
    }
}
