using Microsoft.AspNetCore.Mvc;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Microsoft.EntityFrameworkCore;


namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("Api/Message")]
    
    public class MessageController : ControllerBase
    {
        private readonly AppDbContext context;
        public MessageController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetMensajes()
        {
            var Message = await context.Messages.ToListAsync();
            if (Message == null)
            {
                return NotFound("No se encontraron mensajes");
            }
            if (Message.Count == 0)
            {
                return Ok("No existe Messages por ahora");
            }
            return Ok (Message);
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Message>> GetByid(int Id)
        {
            var messaje = await context.Messages.FirstOrDefaultAsync(x => x.Id == Id);
            if (messaje is null)
            {
                return NotFound($"No existe el Mensaje con id {Id}");
            }
            return Ok(messaje);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Message DTO)
        {
            await context.Messages.AddAsync(DTO);
            await context.SaveChangesAsync();
            return Ok(DTO.Id);
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var existe = await context.Messages.AnyAsync(x => x.Id == Id);
            if (existe == false)
            {
                return NotFound($"No Existe el Mensaje con el Id: {Id}.");
            }
            var messaje = await context.Messages.FirstOrDefaultAsync(x => x.Id == Id);
            context.Messages.Remove(messaje);
            await context.SaveChangesAsync();
            return Ok($"Mensaje con el Id {Id} eliminado correctamente");
        }
        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int Id, Message DTO)
        { if
                (Id != DTO.Id)
            {
                return BadRequest("Datos no validos");
            }
        var existe = await context.Messages.AnyAsync (x => x.Id == Id);
            if(existe !)
            { 
                return NotFound($"No existe el Mensaje con id {Id}");
            }
            context.Update(DTO);
            await context.SaveChangesAsync();
            return Ok($"Mensaje con id {Id} actualizado correctamente");
        }

        

    }
}
