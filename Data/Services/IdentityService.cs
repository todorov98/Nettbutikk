using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nettbutikk.Data.DTO;
using Nettbutikk.Factories;
using Nettbutikk.Models;
using Nettbutikk.State;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.Data.Services
{
    public class IdentityService
    {
        private readonly IdentityContext _context;
        private readonly UserManager<UserEntity> _userManager;
        private readonly DtoMapperService _dtoMapperService;
        private readonly UserFactory _userFactory;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly DeleteUserReceiptFactory _deleteUserReceiptFactory;

        public IdentityService(IdentityContext context, UserManager<UserEntity> userManager, DtoMapperService dtoMapperService,
            UserFactory userFactory, IConfiguration configuration,
            SignInManager<UserEntity> signInManager, DeleteUserReceiptFactory deleteUserReceiptFactory)
        {
            _context = context;
            _userManager = userManager;
            _dtoMapperService = dtoMapperService;
            _userFactory = userFactory;
            _configuration = configuration;
            _signInManager = signInManager;
            _deleteUserReceiptFactory = deleteUserReceiptFactory;
        }

        /// <summary>
        /// Creates a new user on register.
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns>Task.</returns>
        public async Task CreateUserOnRegister(RegisterDTO registerDTO)
        {
            var userEntity = _userFactory.CreateUser();
            userEntity = await _dtoMapperService.MapFromDTO<UserEntity, RegisterDTO>(registerDTO);

            var userExists = await _userManager.FindByNameAsync(userEntity.UserName);

            if (userExists is not null)
                throw new Exception("Username taken.");

            var result = await _userManager.CreateAsync(userEntity, registerDTO.Password);

            if (!result.Succeeded)
            {
                if (result.Errors.Any(err => err.Code.Contains("Password") || err.Code.Contains("password")))
                    throw new Exception("Password rules violation.");

                else throw new Exception("Creation failed.");
            }

            var role = _context.RoleEntities.Where(r => r.Code.Equals(registerDTO.RoleCode))
                .FirstOrDefault() ?? throw new Exception("Invalid role code.");

            await _userManager.AddToRoleAsync(userEntity, role.Name);
        }

        public async Task<string> LoginUser(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.Username) ?? throw new Exception("Username does not exist.");

            if (await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);

                if (!signInResult.Succeeded)
                    throw new Exception("Login failed.");

                return await CreateToken(user);
            }

            else throw new Exception("Incorrect password.");
        }

        public async Task LogoutUser(UserEntity user)
        {
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

            if (_signInManager.IsSignedIn(claimsPrincipal))
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task<UserEntity> GetUserOnUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<DeleteUserReceipt> DeleteUser(string username, UserEntity deletedByUser)
        {
            var userToDelete = await GetUserOnUsername(username);

            var result = await _userManager.DeleteAsync(userToDelete);
            var deletedByAdmin = await _userManager.IsInRoleAsync(deletedByUser, ApplicationRoles.Admin);

            if (result.Succeeded)
            {
                return _deleteUserReceiptFactory.CreateDeleteUserReceipt(username, deletedByAdmin);
            }

            else throw new Exception("Delete user operation failed.");
        }

        private async Task<string> CreateToken(UserEntity user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(7.0).ToString())
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(7.0),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
