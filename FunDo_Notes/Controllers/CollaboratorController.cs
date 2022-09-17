using BuisnessLayer.Interface;
using BuisnessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FunDo_Notes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollaboratorController : Controller
    {
        ICollabBL collabBL;
        public IConfiguration _config;
        public FunDoContext funDoContext;
        public CollaboratorController(ICollabBL collabBL, IConfiguration config, FunDoContext funDoContext)
        {
            this.collabBL = collabBL;
            this._config = config;
            this.funDoContext = funDoContext;
        }
        [Authorize]
        [HttpPost("AddCollaborator/{NoteID}/{CollabEmail}")]
        public async Task<IActionResult> AddCollaborator(int NoteID, string CollabEmail)
        {
            try
            {
                var collabValidate = funDoContext.Notes.Where(n => n.NoteID == NoteID).FirstOrDefault();
                if (collabValidate == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Provide a correct note" });
                }
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                var result=await collabBL.AddCollaborator(UserID, NoteID, CollabEmail);
                if(result==false)
                {
                    return this.BadRequest(new { success = false, status = 400, message = $"{CollabEmail} already a collaborator to this note" });
                }
                return this.Ok(new { success = true, status = 200, message = $"{CollabEmail} successfully added as a collaborator" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("RemoveCollaborator/{NoteID}/{CollaboratorID}")]
        public async Task<IActionResult> RemoveCollaborator(int NoteID, int CollaboratorID)
        {

            try
            {
                var collabValidate = funDoContext.Notes.Where(n => n.NoteID == NoteID).FirstOrDefault();
                if (collabValidate == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Provide a correct note" });
                }
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                //To get Email ID of the Collaborator to be deleted
                var email = (funDoContext.Collaborators.Where(c => c.CollaboratorID == CollaboratorID).FirstOrDefault()).CollabEmail; 
                
                var result=await collabBL.RemoveCollaborator(UserID, NoteID, CollaboratorID);
                if(result==false)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Provide a correct Collaborator " });
                }
                
                return this.Ok(new { success = true, status = 200, message = $"{email} successfully removed as a collaborator from the note" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetCollabs_ByNoteID/{NoteID}")]
        public async Task<IActionResult> GetCollabs_ByNoteID(int NoteID)
        {
            try
            {
                var collabValidate = funDoContext.Notes.Where(n => n.NoteID == NoteID).FirstOrDefault();
                if (collabValidate == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Provide a correct note" });
                }
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                var CollabList=await collabBL.GetCollabs_ByNoteID(UserID, NoteID);
                return this.Ok(new { success = true, status = 200, List= CollabList });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetCollabs_ByUserID")]
        public async Task<IActionResult> GetCollabs_ByUserID()
        {
            try
            {
                /*var collabValidate = funDoContext.Collaborators.Where(u => u.UserId == UserId).FirstOrDefault();
                if (collabValidate == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Provide a correct User Id" });
                }*/
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int USERID = Int32.Parse(userId.Value);
                var CollabList = await collabBL.GetCollabs_ByUserID(USERID);
                return this.Ok(new { success = true, status = 200, List = CollabList });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
