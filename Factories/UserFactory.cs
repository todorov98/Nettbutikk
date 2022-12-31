using Nettbutikk.Models;

namespace Nettbutikk.Factories
{
    public class UserFactory
    {
        public UserEntity CreateUser()
        {
            return new UserEntity();
        }
    }
}
