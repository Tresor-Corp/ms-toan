using Demo.Core.Entity;
using Demo.Core.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Command
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
