using Demo.Application.Query;
using Demo.Core.Entity;
using Demo.Core.IRepository;
using Demo.Core.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Handler
{
    public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, BaseResponse<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserProfileHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<User>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserProfile(request.Id);
        }
    }
}
