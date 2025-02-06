using Darts.DAL;
using Darts.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Darts.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IUnitOfWork db;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(IUnitOfWork db, ILogger<PlayerController> logger)
        {
            this.db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Player>> Get()
        {
            return await db.Players.GetAll();
        }

        [HttpPost]
        public async Task<ActionResult<Player>> Create(string name)
        {
            Player newPlayer = new Player() { Name = name };
            await db.Players.Add(newPlayer);
            await db.CompleteAsync();
            return Ok(newPlayer);
        }

        [HttpDelete]
        public async Task<IActionResult>Delete(int id)
        {
            await db.Players.Delete(id);
            await db.CompleteAsync();
            return NoContent();
        }
    }
}
