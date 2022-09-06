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
        public LabelController(ILabelBL labelBl,IConfiguration _config, FunDoContext funDoContext)
        {
            this.labelBl = labelBl;
            this._config = _config;
            this.funDoContext = funDoContext;
        }
        [Authorize]
        [HttpPost("AddLabel/{NoteID}/{LabelName}")]
        public async Task<IActionResult> AddLabel(int NoteID,string LabelName)
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
    }
}
