using JwtAuth.Core.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.Application.Query
{
    public class LoginQuery : IRequest<BaseResponse<TokenResponse>>
    {
        [NotNull]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [NotNull]
        public string Password { get; set; }
    }
}
