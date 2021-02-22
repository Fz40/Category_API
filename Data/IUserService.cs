using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Data
{
    public interface IUserService
    {
        bool SaveChanges();
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserById(string Id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Task<IEnumerable<User>> GetUserByCondition(ConditionModel condition);

    }
}