using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext context;

        public UserController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            var users = await context.Users.ToListAsync();
        }
    }
}
