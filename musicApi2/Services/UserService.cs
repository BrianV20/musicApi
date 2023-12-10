using AutoMapper;
using Microsoft.EntityFrameworkCore;
using musicApi2.Models.User;
using musicApi2.Models.User.Dto;
using System.Linq.Expressions;

namespace musicApi2.Services
{
    public class UserService : IUserInterface
    {
        private readonly musicApiContext _context;
        private readonly IMapper _mapper;

        public UserService(musicApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IEnumerable<User>> Add(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            _context.Users.Add(user);
            //var userToCreate = _mapper.Map<CreateUserDto>(user);
            await Save();
            return await _context.Users.ToListAsync();
        }


        public async Task Delete(int id)
        {
            var userToDelete = _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            _context.Users.Remove(await userToDelete);
            await Save();
        }


        public async Task<IEnumerable<UsersDto>> GetAll(Expression<Func<User, bool>>? filter = null)
        {
            var users = _context.Users.AsQueryable();
            if (filter != null)
            {
                users = users.Where(filter);
            }
            var result = await users.ToListAsync();
            var usersDto = _mapper.Map<IEnumerable<UsersDto>>(result);
            return usersDto;
        }


        public async Task<User> GetOne(Expression<Func<User, bool>>? filter = null)
        {
            if(filter != null)
            {
                User user = await _context.Users.FirstOrDefaultAsync(filter);
                return user;
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
                User userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == id); //aca traigo al usuario que
                                                                                           //quiero actualizar
                var userUpdated = _mapper.Map(updateUserDto, userToUpdate); //aca actualizo el usuario
                _context.Users.Update(userUpdated); //aca lo guardo en la base de datos
                await Save(); //aca guardo los cambios
                return _mapper.Map<UserDto>(userUpdated); //aca devuelvo el usuario actualizado en formato UserDto
            }
            catch
            {
                return null;
            }
        }
    }
}
