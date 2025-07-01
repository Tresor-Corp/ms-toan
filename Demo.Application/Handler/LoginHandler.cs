using Demo.Application.Query;
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
    public class LoginHandler : IRequestHandler<LoginQuery, BaseResponse<TokenResponse>>
    {
        private readonly IUserRepository _userRepository;

        public LoginHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<TokenResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.Login(request.Email, request.Password);
        }
    }
}
