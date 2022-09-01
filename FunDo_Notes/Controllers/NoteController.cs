using BuisnessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using System;
using System.Linq;

namespace FunDo_Notes.Controllers
{
    public class NoteController : Controller
    {
        INoteBL noteBL;
        private IConfiguration _config;
        public FunDoContext funDoContext;
        public NoteController(INoteBL noteBL, IConfiguration config, FunDoContext funDoContext)
        {
            this.noteBL = noteBL;
            _config = config;
            this.funDoContext = funDoContext;
        }
        [Authorize]
        [HttpPost("AddNote")]
        public IActionResult AddNote(NoteModel noteModel)
        {
            try
            {
                //Authorization, match userId from token
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                this.noteBL.AddNote(noteModel, UserID);
                return this.Ok(new { success = true, status = 200, message = "Note Added successfully" });
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
