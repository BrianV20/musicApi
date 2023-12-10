using musicApi2.Models.User;
using musicApi2.Models.User.Dto;
using System.Linq.Expressions;

namespace musicApi2.Services
{
    public interface IUserInterface
    {
        Task<IEnumerable<User>> Add(CreateUserDto e);

        Task<UserDto> Update(int id, UpdateUserDto e);

        Task Delete(int id);

        Task<User> GetOne(Expression<Func<User, bool>>? filter = null);

        Task<IEnumerable<UsersDto>> GetAll(Expression<Func<User, bool>>? filter = null);

        Task Save();
    }
}
