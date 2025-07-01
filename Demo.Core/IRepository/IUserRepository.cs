using Demo.Core.Entity;
using Demo.Core.Response;

namespace Demo.Core.IRepository
{
    public interface IUserRepository
    {
        Task<BaseResponse<TokenResponse>> Login(string email, string password);
        Task<BaseResponse<User>> Regiter(User user);
        Task<BaseResponse<User>> GetUserProfile(Guid userId);
    }
}
