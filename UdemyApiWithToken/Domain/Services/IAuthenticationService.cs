using UdemyApiWithToken.Domain.Responses;
using UdemyApiWithToken.Security.Token;

namespace UdemyApiWithToken.Domain.Services
{
    public interface IAuthenticationService
    {
        BaseResponse<AccessToken> CreateAccessToken(string email, string password);

        BaseResponse<AccessToken> CreateAccessTokenByRefreshToken(string refreshToken);

        BaseResponse<AccessToken> RevokeRefreshToken(string refreshToken);
    }
}