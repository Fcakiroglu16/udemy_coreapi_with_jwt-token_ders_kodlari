using UdemyApiWithToken.Domain.Responses;

namespace UdemyApiWithToken.Domain.Services
{
    public interface IUserService
    {
        BaseResponse<User> AddUser(User user);

        BaseResponse<User> FindById(int userId);

        BaseResponse<User> FindEmailAndPassword(string email, string password);

        void SaveRefreshToken(int userId, string refreshToken);

        BaseResponse<User> GetUserWithRefreshToken(string refreshToken);

        void RemoveRefreshToken(User user);
    }
}