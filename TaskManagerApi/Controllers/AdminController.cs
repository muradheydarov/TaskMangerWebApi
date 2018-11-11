using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Model;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagerApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        TaskManagerContext _db;
        public AdminController(TaskManagerContext taskManager)
        {
            _db = taskManager;
        }       

        //api/createdocument
        [HttpPost]
        [Route("createdocument")]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentInfoFromBody documentInfoBody)
        {
            var usernameLowerCare = documentInfoBody.UserName.ToLower();
            var jsonResult = await _db.Users                                        
                                        .SingleOrDefaultAsync(x => x.Username == usernameLowerCare);

            if (jsonResult!=null)
            {
                DocumentInfo documentInfo = new DocumentInfo()
                {
                    SN = documentInfoBody.SN,
                    ImportExport = documentInfoBody.ImportExport,
                    GbNumber = documentInfoBody.GbNumber,
                    GbState = documentInfoBody.GbState,
                    User = _db.Users.FirstOrDefault(x => x.Username == documentInfoBody.UserName.ToLower())
                };
                await _db.DocumentInfos.AddAsync(documentInfo);
                await _db.SaveChangesAsync();

                return Ok();
            }
            
            return BadRequest("User not exist");
            
        }

        [HttpPost]
        [Route("deletedocument/{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {            
            var document = await _db.DocumentInfos                                        
                                        .FirstOrDefaultAsync(x => x.Id == id);
            document.Active = false;
            await _db.SaveChangesAsync();

            return Ok();
        }

        //api/users
        [HttpGet]
        [Route("users")]
        public async Task<JsonResult> GetUsers()
        {
            var jsonResult = await _db.Users
                                        .Include(x => x.DocumentInfos)
                                        .ToListAsync();

            return new JsonResult(jsonResult);
        }

        /// <summary>
        /// get all documents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("documents")]
        public async Task<JsonResult> GetDocuments()
        {
            var jsonResult = await _db.DocumentInfos                                        
                                        .ToListAsync();

            return new JsonResult(jsonResult);
        }
    }
}