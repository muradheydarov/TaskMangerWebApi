using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Model;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        TaskManagerContext _db;
        public AdminController(TaskManagerContext taskManager)
        {
            _db = taskManager;
        }

        //api/admin/createuser?username=""
        [HttpGet]
        [Route("createuser")]
        public async Task<IActionResult> CreateUser([FromQuery(Name = "username")] string username)
        {
            User user = new User() { Username = username };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return Ok("user saved successfully");
        }

        //api/createdocument
        [HttpPost]
        [Route("createdocument")]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentInfoFromBody documentInfoBody)
        {
            DocumentInfo documentInfo = new DocumentInfo()
            {
                SN = documentInfoBody.SN,
                ImportExport = documentInfoBody.ImportExport,
                GbNumber = documentInfoBody.GbNumber,
                GbState = documentInfoBody.GbState,
                User = _db.Users.FirstOrDefault(x => x.Username == documentInfoBody.UserName)
            };
            await _db.DocumentInfos.AddAsync(documentInfo);
            await _db.SaveChangesAsync();

            return Ok();
        }

        //api/users
        [HttpGet]
        [Route("users")]
        public async Task<JsonResult> GetDocuments()
        {
            var jsonResult = await _db.Users
                                        .Include(x => x.DocumentInfos)
                                        .ToListAsync();

            return new JsonResult(jsonResult);
        }
    }
}