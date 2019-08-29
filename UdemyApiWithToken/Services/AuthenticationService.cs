using System;
using UdemyApiWithToken.Domain;
using UdemyApiWithToken.Domain.Responses;
using UdemyApiWithToken.Domain.Services;
using UdemyApiWithToken.Security.Token;

namespace UdemyApiWithToken.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService userService;

        private readonly ITokenHandler tokenHandler;

        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler)
        {
            this.userService = userService;
            this.tokenHandler = tokenHandler;
        }

        public BaseResponse<AccessToken> CreateAccessToken(string email, string password)
        {
            BaseResponse<User> userResponse = userService.FindEmailAndPassword(email, password);

            if (userResponse.Success)
            {
                AccessToken accessToken = tokenHandler.CreateAccessToken(userResponse.Extra);

                userService.SaveRefreshToken(userResponse.Extra.Id, accessToken.RefreshToken);

                return new BaseResponse<AccessToken>(accessToken);
            }
            else
            {
                return new BaseResponse<AccessToken>(userResponse.ErrorMessage);
            }
        }

        public BaseResponse<AccessToken> CreateAccessTokenByRefreshToken(string refreshToken)
        {
            BaseResponse<User> userResponse = userService.GetUserWithRefreshToken(refreshToken);

            if (userResponse.Success)
            {
                if (userResponse.Extra.RefreshTokenEndDate > DateTime.Now)
                {
                    AccessToken accessToken = tokenHandler.CreateAccessToken(userResponse.Extra);

                    userService.SaveRefreshToken(userResponse.Extra.Id, accessToken.RefreshToken);

                    return new BaseResponse<AccessToken>(accessToken);
                }
                else
                {
                    return new BaseResponse<AccessToken>("refreshtoken suresi dolmus");
                }
            }
            else
            {
                return new BaseResponse<AccessToken>("refreshtoken bulunamadı");
            }
        }

        public BaseResponse<AccessToken> RevokeRefreshToken(string refreshToken)
        {
            BaseResponse<User> userResponse = userService.GetUserWithRefreshToken(refreshToken);

            if (userResponse.Success)
            {
                userService.RemoveRefreshToken(userResponse.Extra);

                return new BaseResponse<AccessToken>(new AccessToken());
            }
            else
            {
                return new BaseResponse<AccessToken>("refreshtoken bulunamadı.");
            }
        }
    }
}