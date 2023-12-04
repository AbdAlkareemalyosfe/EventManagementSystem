using EventManagementSystemApi.DbContext;
using EventManagementSystemApi.Interface;
using EventManagementSystemApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManagementSystemApi.Repository
{


    public class UserManager : IUserManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Jwt _jwt;
        public UserManager(IOptions<Jwt> jwt, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _jwt = jwt.Value;
        }

        // Method to register a new user
        public string RegisterUser(string name, string email, string password)
        {
            User newUser = new() { Name = name, Email = email, Password = password };
            newUser.Token = GenerateToken(newUser.Id);
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return newUser.Token;

        }

        // Method to authenticate a user
        public string AuthenticateUser(string email, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user != null ? user.Token : "User Not Register";
        }

        // Method to get a user by email
        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }
        public List<object> GetUsers()
        {
            var userList = _dbContext.Users
                .Select(x => new { x.Id, x.Name, x.Email })
                .ToList<object>();

            return userList;
        }

        private string GenerateToken(int userId)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}


