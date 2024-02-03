using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using musicApi2.Models.User;
using musicApi2.Models.User.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace musicApi2.Services
{
    public interface IUserInterface
    {
        Task<UserDto> Add(CreateUserDto e);

        Task<UserDto> Update(int id, UpdateUserDto e);

        Task Delete(int id);

        Task<UserDto> GetOne(Expression<Func<User, bool>>? filter = null);

        Task<IEnumerable<UserDto>> GetAll(Expression<Func<User, bool>>? filter = null);

        Task Save();

        Task<string> Login(LoginUserDto loginUserDto);

        Task<string> verifyToken(string token);

        Task<UserDto> getUserFromToken(string token);
    }
    public class UserService : IUserInterface
    {
        private readonly musicApiContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(musicApiContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }


        public async Task<UserDto> Add(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            _context.Users.Add(user);
            await Save();
            return _mapper.Map<UserDto>(user);
        }


        public async Task Delete(int id)
        {
            var userToDelete = _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            _context.Users.Remove(await userToDelete);
            await Save();
        }


        public async Task<IEnumerable<UserDto>> GetAll(Expression<Func<User, bool>>? filter = null)
        {
            var users = _context.Users.AsQueryable();
            if (filter != null)
            {
                users = users.Where(filter);
            }
            var result = await users.ToListAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(result);
            return usersDto;
        }


        public async Task<UserDto> GetOne(Expression<Func<User, bool>>? filter = null)
        {
            if(filter != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(filter);
                return _mapper.Map<UserDto>(user);
            }
            return null;
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<UserDto> Update(int id, UpdateUserDto updateUserDto)
        {
            try
            {
                var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == id); //aca traigo al usuario que
                                                                                           //quiero actualizar
                if (userToUpdate != null)
                {
                    var user = _mapper.Map(updateUserDto, userToUpdate); //aca actualizo el usuario
                    _context.Users.Update(user); //aca lo guardo en la base de datos
                    await Save(); //aca guardo los cambios
                    return _mapper.Map<UserDto>(user); //aca devuelvo el usuario actualizado en formato UserDto
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> Login(LoginUserDto loginUserDto)
        {
            var user = await ValidateUser(loginUserDto);
            if (user != null)
            {
                return GenerateToken(user);
            }

            throw new Exception("Invalid credentials");
        }

        private async Task<User> ValidateUser(LoginUserDto loginUserDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUserDto.Email && u.Password == loginUserDto.Password);
            if (user != null)
            {
                return user;
            }

            return null;
        }

        private string GenerateToken(User user)
        {
            // JWT authorization key generation
            //var claims = new[]
            //{
            //    new Claim("email", user.Email),
            //    new Claim("id", user.Id.ToString())
            //};

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var token = new JwtSecurityToken(
            //    issuer: _configuration["JwtConfig:Issuer"],
            //    audience: _configuration["JwtConfig:Audience"],
            //    claims: claims,
            //    expires: DateTime.Now.AddMinutes(30),
            //    signingCredentials: creds
            //    );

            //return new JwtSecurityTokenHandler().WriteToken(token);


            // Basic authorization key generation
            var authInfo = $"{user.Email}:{user.Password}";
            var authInfoBytes = Encoding.UTF8.GetBytes(authInfo);
            return Convert.ToBase64String(authInfoBytes);
        }

        public async Task<string> verifyToken(string token)
        {
            // Basic authorization key validation
            try
            {
                var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var emailPassword = decodedToken.Split(":");

                if(emailPassword.Length != 2)
                {
                    return null;
                }

                var email = emailPassword[0];
                var password = emailPassword[1];

                var user = await ValidateUser(new LoginUserDto { Email = email, Password = password });
                if(user == null)
                {
                    return null;
                }

                if(user.Email != email && user.Password != password)
                {
                    return null;
                }

                return token;
            }
            catch
            {
                return null;
            }

            // JWT authorization key validation
            //try
            //{
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
            //    tokenHandler.ValidateToken(token, new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = key,
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ClockSkew = TimeSpan.Zero   
            //    }, out SecurityToken validatedToken);
            //    var jwtToken = (JwtSecurityToken)validatedToken;
            //    return jwtToken;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public async Task<UserDto> getUserFromToken(string token)
        {
            try
            {
                var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var emailPassword = decodedToken.Split(":");
                var email = emailPassword[0];
                var password = emailPassword[1];
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                return _mapper.Map<UserDto>(user);
                //var tokenHandler = new JwtSecurityTokenHandler();
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
                //tokenHandler.ValidateToken(token, new TokenValidationParameters
                //{
                //    ValidateIssuerSigningKey = true,
                //    IssuerSigningKey = key,
                //    ValidateIssuer = false,
                //    ValidateAudience = false,
                //    ClockSkew = TimeSpan.Zero   
                //}, out SecurityToken validatedToken);
                //var jwtToken = (JwtSecurityToken)validatedToken;
                //var email = token.Claims.First(x => x.Type == "email").Value;
                //var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                //return _mapper.Map<UserDto>(user);
            }
            catch
            {
                return null;
            }
        }
    }
}
