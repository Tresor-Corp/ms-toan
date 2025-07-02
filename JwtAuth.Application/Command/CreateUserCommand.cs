using JwtAuth.Core.Entity;
using JwtAuth.Core.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace JwtAuth.Application.Command
{
    public class CreateUserCommand : IRequest<BaseResponse<User>>
    {
        [NotNull]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [NotNull]
        public string Password { get; set; }
        [Phone(ErrorMessage = "Phone is not valid")]
        public string PhoneNumber { get; set; }
    }
}
