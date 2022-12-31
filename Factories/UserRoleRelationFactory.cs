using Nettbutikk.Models;

namespace Nettbutikk.Factories
{
    public class UserRoleRelationFactory
    {
        public UserRoleRelationEntity CreateUserRoleRelation(string userId, string roleId) 
        {
            return new UserRoleRelationEntity { UserId = userId, RoleId = roleId };
        }
    }
}
