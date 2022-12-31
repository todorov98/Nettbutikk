using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nettbutikk.Data;
using System;

namespace Nettbutikk.Models
{
    public class UserEntity : IdentityUser, IEntity
    {
        private readonly IdentityContext _identityContext;
        private readonly ILogger<UserEntity> _errorLogger;

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserEntity(IdentityContext identityContext, ILogger<UserEntity> errorLogger)
        {
            _identityContext = identityContext;
            _errorLogger = errorLogger;
        }

        public UserEntity(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }

        public UserEntity()
        {

        }

        public bool Commit()
        {
            try
            {
                if (Exists())
                {
                    _identityContext.SaveChanges();
                    return true;
                }

                else
                {
                    _identityContext.UserEntities.Add(this);
                    _identityContext.SaveChanges();
                    return true;
                }
            }

            catch (Exception e)
            {
                _errorLogger.LogError($"Error location: {e.StackTrace}, Exception type: {e.GetType()}, " +
                    $"Error message: {e.Message}");

                throw new Exception("ERROR: Order commit failed.");
            }
        }

        public bool Exists()
        {
            try
            {
                if (_identityContext.UserEntities.Find(Id) != null)
                    return true;

                else return false;
            }

            catch (Exception e)
            {
                _errorLogger.LogError($"Error location: {e.StackTrace}, Exception type: {e.GetType()}, " +
                    $"Error message: {e.Message}");

                throw new Exception("ERROR: Order existence check failed.");
            }
        }
    }
}
