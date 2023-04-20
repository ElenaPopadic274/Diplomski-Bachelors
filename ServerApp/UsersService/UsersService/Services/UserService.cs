using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UsersService.DTO;
using UsersService.Infrastracture;
using UsersService.Interfaces;
using UsersService.Models;

namespace UsersService.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UsersDbContext _dbContext;
        private readonly IConfigurationSection _secretKey;
        private readonly IConfigurationSection _tokenAddress;
        private const string _pepper = "aasf3rko3W";

        string Encode(string raw)
        {
            using (var sha = SHA256.Create())
            {
                var computedHash = sha.ComputeHash(
                Encoding.Unicode.GetBytes(raw + _pepper));
                return Convert.ToBase64String(computedHash);
            }
        }
        public UserService(IMapper mapper, UsersDbContext dbContext, IConfiguration config)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _secretKey = config.GetSection("SecretKey");
            _tokenAddress = config.GetSection("tokenAddress");

        }

        public bool AddUser(UserDto userdb)
        {
            try
            {
                var users = _dbContext.Users.Where(s => s.Username == userdb.Username).ToList();
                if (users.Count != 0)
                    return false;
                userdb.Password = Encode(userdb.Password);
                if (userdb.Type.ToLower() != "user")
                {
                    userdb.Activated = false;
                    userdb.Status = DelivererStatus.NOTVERIFIED;
                }
                else
                    userdb.Activated = true;
                User user = _mapper.Map<User>(userdb);
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {

                return false;
            }

            return true;
        }

        public UserDto FindById(long id)
        {
            return _mapper.Map<UserDto>(_dbContext.Users.Find(id));
        }

        public UserDto FindByUsername(string username)
        {
            return _mapper.Map<UserDto>(_dbContext.Users.Where(s => s.Username == username).ToList()[0]);
        }

        public List<UserDto> GetUsers()
        {
            List<UserDto> users = _mapper.Map<List<UserDto>>(_dbContext.Users.ToList());
            foreach (var item in users)
            {
                item.Password = "";
            }
            return users;
        }

        public TokenDto Login(LoginDto user)
        {
            var users = _dbContext.Users.Where(s => s.Username == user.Username).Where(x => x.Password == Encode(user.Password)).ToList();
            if (users.Count == 0)
                return null;
            List<Claim> claims = new List<Claim>();
            if (users[0].Activated)
                claims.Add(new Claim("username", users[0].Username));
            claims.Add(new Claim("id", users[0].Id.ToString()));
            claims.Add(new Claim("role", users[0].Type));
            claims.Add(new Claim("isActivated", users[0].Activated.ToString()));
            claims.Add(new Claim("Status", users[0].Status.ToString()));
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(//kreiranje JWT
                   issuer: _tokenAddress.Value, //url servera koji je izdao token
                   claims: claims, //claimovi
                   expires: DateTime.Now.AddMinutes(20), //vazenje tokena u minutama
                   signingCredentials: signinCredentials //kredencijali za potpis
               );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return new TokenDto() { Token = tokenString };
        }

        public bool ModifyUser(UserDto userdto)
        {
            User user = _dbContext.Users.Find(userdto.Id); //Ucitavamo objekat u db context (ako postoji naravno)
            if (user == null)
                return false;
            user.Firstname = userdto.Firstname;
            user.Firstname = userdto.Firstname;
            user.Lastname = userdto.Lastname;
            if (userdto.Password.Length > 4)
                user.Password = Encode(userdto.Password);
            user.Username = userdto.Username;

            _dbContext.SaveChanges();

            return true;
        }


        public List<UserDto> Unactivated()
        {
            return _mapper.Map<List<UserDto>>(_dbContext.Users.Where(x => x.Status == DelivererStatus.NOTVERIFIED).ToList());
        }

        public bool VerifyUser(long id)
        {
            User user = _dbContext.Users.Find(id);
            if (user == null)
                return false;
            user.Activated = true;
            user.Status = DelivererStatus.VERIFIED;
            _dbContext.SaveChanges();
            return true;
        }

        public bool DismissUser(long id)
        {

            User user = _dbContext.Users.Find(id);
            if (user == null)
                return false;
            user.Activated = false;
            user.Status = DelivererStatus.DENIED;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
