using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Api.Filters
{
    public class JwtAuthenticationFilterAttribute : Attribute, IAuthenticationFilter
    {
        private readonly string _jwtSecret = ConfigurationManager.AppSettings["JwtSecret"];

        public bool AllowMultiple => false;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (context.ActionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                (!context.ActionContext.ActionDescriptor.GetCustomAttributes<AuthorizeAttribute>().Any() &&
                !context.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AuthorizeAttribute>().Any()))
            {
                return Task.CompletedTask;
            }

            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer" || string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return Task.CompletedTask;
            }

            string jwtToken = authorization.Parameter;
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

                var principal = tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out var validatedToken);
                context.Principal = principal;
            }
            catch (Exception)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
            }

            return Task.CompletedTask;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
