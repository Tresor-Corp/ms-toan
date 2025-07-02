using JwtAuth.Application.Query;
using JwtAuth.Core.IRepository;
using JwtAuth.Core.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.Application.Handler
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
