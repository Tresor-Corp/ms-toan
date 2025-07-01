using Demo.Core.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Query
{
    public class LoginQuery : IRequest<BaseResponse<TokenResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
