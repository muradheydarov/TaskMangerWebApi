using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Model;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        TaskManagerContext _db;
        public UserController(TaskManagerContext db)
        {
            _db = db;
        }

        //api/editstatus
        [HttpGet]
        [Route("editstatus")]
        public async Task<IActionResult> CreateUser([FromQuery] int id, string GBState)
        {            
            var document = await _db.DocumentInfos.FirstOrDefaultAsync(x => x.Id == id);
            document.GbState = GBState;
            await _db.SaveChangesAsync();

            return Ok();
        }

        //api/getuserinfo?username=""
        [HttpGet()]
        [Route("getuserinfo")]
        public async Task<JsonResult> GetUser([FromQuery(Name = "username")] string username)
        {
            var jsonResult = await _db.Users
                                        .Include(x => x.DocumentInfos)
                                        .FirstOrDefaultAsync(x => x.Username==username);

            return new JsonResult(jsonResult);
        }
    }
}