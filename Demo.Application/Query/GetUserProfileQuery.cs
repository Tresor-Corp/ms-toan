using Demo.Core.Entity;
using Demo.Core.Response;
using MediatR;

namespace Demo.Application.Query
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
