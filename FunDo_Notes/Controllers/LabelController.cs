using BuisnessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace FunDo_Notes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LabelController : Controller
    {
        ILabelBL labelBl;
        private IConfiguration _config;
        public FunDoContext funDoContext;
        public LabelController(ILabelBL labelBl, IConfiguration _config, FunDoContext funDoContext)
        {
            this.labelBl = labelBl;
            this._config = _config;
            this.funDoContext = funDoContext;
        }
        [Authorize]
        [HttpPost("AddLabel/{NoteID}/{LabelName}")]
        public async Task<IActionResult> AddLabel(int NoteID, string LabelName)
        {
            try
            {
                var note = funDoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Provide a correct note" });
                }
                await this.labelBl.AddLabelNote(UserID, NoteID, LabelName);
                return this.Ok(new { success = true, status = 200, message = "Label added successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("UpdateLabel/{NoteID}/{LabelName}")]
        public async Task<IActionResult> Update_NoteLabel(int NoteID, string LabelName)
        {
            try
            {
                var note = funDoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Provide a correct note" });
                }
                var res = await this.labelBl.Update_NoteLabel(UserID, NoteID, LabelName);
                if (res == true)
                {
                    return this.Ok(new { success = true, status = 200, message = "Label updated successfully" });
                }
                return this.BadRequest(new { success = false, status = 400, message = "Note in the trash folder ..... Label cannot be updated" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteLabel/{NoteID}")]
        public async Task<IActionResult> Delete_NoteLabel(int NoteID)
        {
            try
            {
                var note = funDoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Provide a correct note" });
                }
                var res = await this.labelBl.Delete_NoteLabel(UserID, NoteID);
                if (res == true)
                {
                    return this.Ok(new { success = true, status = 200, message = "Label deleted successfully" });
                }
                return this.BadRequest(new { success = false, status = 400, message = "Note in the trash folder " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GeAllNoteLabel/{NoteID}")]
        public async Task<IActionResult> GetLabelByNoteID(int NoteID)
        {
            try
            {
                var note = funDoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Provide a correct note" });
                }
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                var labels = await this.labelBl.GetLabelByNoteID(UserID, NoteID);
                return this.Ok(new { success = true, status = 200, Labels = labels });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
