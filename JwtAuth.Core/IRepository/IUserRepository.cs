using JwtAuth.Core.Entity;
using JwtAuth.Core.Response;

namespace JwtAuth.Core.IRepository
{
    public interface IUserRepository
    {
        Task<BaseResponse<TokenResponse>> Login(string email, string password);
        Task<BaseResponse<User>> Regiter(User user);
        Task<BaseResponse<User>> GetUserProfile(Guid userId);
    }
}
