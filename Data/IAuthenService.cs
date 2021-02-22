
using API.Models;

namespace API.Data
{
    public interface IAuthenService
    {
        User AuthenticateUser(User login);
        string GennerateJSONWebToken(User userinfo);
    }
}