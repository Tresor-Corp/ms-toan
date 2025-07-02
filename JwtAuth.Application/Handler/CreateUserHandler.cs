using JwtAuth.Application.Command;
using JwtAuth.Core.Entity;
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
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse<User>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.Regiter(new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
            });
        }
    }
}
