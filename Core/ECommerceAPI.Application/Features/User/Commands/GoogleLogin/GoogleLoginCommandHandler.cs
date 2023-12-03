using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Application.Features.User.Commands.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {

        readonly UserManager<AppUser> _userManager;
        readonly IConfiguration _configuration;
        readonly ITokenHandler _tokenHandler;
        public GoogleLoginCommandHandler(UserManager<AppUser> userManager, IConfiguration configuration,ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            string clientId = _configuration["GoogleLoginInfo:ClientId"];
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { clientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
            var userLoginInfo = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
            AppUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NameSurname = payload.Name,
                        
                    };

                    IdentityResult createUser = await _userManager.CreateAsync(user);
                    result = createUser.Succeeded;

                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, userLoginInfo);
            }
            else
            {
                throw new Exception("Invalid external authentication");
            }

            Token token = _tokenHandler.CreateAccessToken(5);

            return new()
            {
                Token = token
            };
        }
    }
}
