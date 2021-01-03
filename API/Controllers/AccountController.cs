using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly DataContext _dataContext;
        public ITokenService _tokenService { get; }

        public AccountController(DataContext dataContext, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _dataContext = dataContext;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto register)
        {
            if (await BoolRecordExists(register.Username.ToLower())) return BadRequest("User already exists");

            using var hmac = new HMACSHA512();
            var user = new AppUser();
            user.Username = register.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.password));
            user.PasswordSalt = hmac.Key;
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return new UserDto{
                Username = user.Username,
                TokenKey = _tokenService.CreateServie(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            AppUser user = await _dataContext.Users.SingleOrDefaultAsync(x => x.Username == loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Invaid Username");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDto{
                Username = user.Username,
                TokenKey = _tokenService.CreateServie(user)
            };
        }

        private async Task<bool> BoolRecordExists(string username)
        {
            return await _dataContext.Users.AnyAsync(user => user.Username == username.ToLower());
        }
    }
}