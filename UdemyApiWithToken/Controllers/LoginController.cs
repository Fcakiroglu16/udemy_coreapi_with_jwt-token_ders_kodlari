using Microsoft.AspNetCore.Mvc;
using UdemyApiWithToken.Domain.Responses;
using UdemyApiWithToken.Domain.Services;
using UdemyApiWithToken.Extensions;
using UdemyApiWithToken.Resources;
using UdemyApiWithToken.Security.Token;

namespace UdemyApiWithToken.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Accesstoken(LoginResource loginResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                BaseResponse<AccessToken> accessTokenResponse = authenticationService.CreateAccessToken(loginResource.Email, loginResource.Password);

                if (accessTokenResponse.Success)
                {
                    return Ok(accessTokenResponse.Extra);
                }
                else
                {
                    return BadRequest(accessTokenResponse.ErrorMessage);
                }
            }
        }

        [HttpPost]
        public IActionResult RefreshToken(TokenResource tokenResource)
        {
            BaseResponse<AccessToken> accessTokenResponse = authenticationService.CreateAccessTokenByRefreshToken(tokenResource.RefreshToken);

            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.Extra);
            }
            else
            {
                return BadRequest(accessTokenResponse.ErrorMessage);
            }
        }

        [HttpPost]
        public IActionResult RevokeRefreshToken(TokenResource tokenResource)
        {
            BaseResponse<AccessToken> accessTokenResponse = authenticationService.RevokeRefreshToken(tokenResource.RefreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.Extra);
            }
            else
            {
                return BadRequest(accessTokenResponse.ErrorMessage);
            }
        }
    }
}