using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using musicApi2.Attributes;
using musicApi2.Models.User;
using musicApi2.Services;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace musicApi2.Controllers
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly musicApiContext _context;

        public BasicAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock, 
            musicApiContext context
            ) : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // check if request has authorization header
            if(!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization header was not found");
            }

            User result;

            try
            {
                // get authorization header
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                // decode header
                var credentialsBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(":");
                var email = credentials[0];
                var password = credentials[1];
                result = await getUser(email, password);
            }
            catch
            {
                return AuthenticateResult.Fail("Error has ocurred");
            }

            if(result == null)
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }
            else
            {
                var client = new BasicAuthenticationClient
                {
                    AuthenticationType = "Basic",
                    IsAuthenticated = true,
                    Name = result.Username
                };

                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                    new Claim(ClaimTypes.Name, result.Email)
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
        }


        private async Task<bool> ValidateUser(string email, string password)
        {
            var user = await getUser(email, password);
            if(user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<User> getUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if(user != null)
            {
                return user;
            }
            return null;
        }
    }
}
