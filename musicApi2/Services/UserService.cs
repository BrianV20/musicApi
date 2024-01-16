using AutoMapper;
using Microsoft.EntityFrameworkCore;
using musicApi2.Models.User;
using musicApi2.Models.User.Dto;
using System.Linq.Expressions;

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
    }
    public class UserService : IUserInterface
    {
        private readonly musicApiContext _context;
        private readonly IMapper _mapper;

        public UserService(musicApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
    }
}
