using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route(routeName)]
    public class UsersController : ControllerBase
    {
        private const string routeName = "api/Users";
        private readonly DataContext _dataContext;

        public UsersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            return await _dataContext.Users.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUsersById(int id){
            return await _dataContext.Users.FindAsync(id);
        }
    }
}