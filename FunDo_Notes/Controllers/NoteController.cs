using BuisnessLayer.Interface;
using CommonLayer;
using CommonLayer.Notes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunDo_Notes.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [Authorize]
        [HttpPut("UpdateNote/{NoteID}")]
        public IActionResult UpdateNote(UpdateNoteModel updateNoteModel, int NoteID)
        {
            try
            {
                var note = funDoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "Provide a correct note" });
                }
                this.noteBL.UpdateNote(updateNoteModel, UserID, NoteID);
                return this.Ok(new { success = true, status = 200, message = "Note Updated successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteNote/{NoteID}")]
        public IActionResult DeleteNote(int NoteID)
        {
            try
            {
                var deleteNote = funDoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (deleteNote == null)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "Provide an existing note" });
                }
                bool answer = this.noteBL.DeleteNote(UserID, NoteID);
                return this.Ok(new { success = true, status = 200, message = "Note deleted successfully" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetNote/{NoteID}")]
        public IActionResult GetNote(int NoteID)
        {
            try
            {
                Note specificNote = new Note();
                var getNote = funDoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (getNote == null)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "Provide an existing Note ID" });
                }
                specificNote = this.noteBL.GetNote(UserID, NoteID);
                return this.Ok(new { success = true, status = 200, note = specificNote });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllNote")]
        public IActionResult GetAllNotes()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                List<NoteResponseModel> note = new List<NoteResponseModel>();
                note=this.noteBL.GetAllNotes(UserID);
                return this.Ok(new { success = true, status = 200, noteList = note });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("Archive/{NoteID}")]
        public async Task<IActionResult> ArchiveNote(int NoteID)
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
                var Archive = await this.noteBL.ArchiveNote(UserID, NoteID);
                return this.Ok(new { success = true, status = 200, message = "Archive toggle button pressed successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("Pin_Note/{NoteID}")]
        public async Task<IActionResult> PinNote(int NoteID)
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
                var Pin = await this.noteBL.PinNote(UserID, NoteID);
                return this.Ok(new { success = true, status = 200, message = "Pin toggle button pressed successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("Trash/{NoteID}")]
        public async Task<IActionResult> Trash_Note(int NoteID)
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
                var Trash = await this.noteBL.Trash_Note(UserID, NoteID);
                return this.Ok(new { success = true, status = 200, message = " Toggle button pressed successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("Reminder/{NoteID}")]
        public async Task<IActionResult> ReminderNote(int NoteID,NoteReminderModel reminder)
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
                var remind = Convert.ToDateTime(reminder.Reminder);
                await this.noteBL.ReminderNote(UserID, NoteID,remind);
                return this.Ok(new { success = true, status = 200, message = " Note reminder added successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteReminder/{NoteID}")]
        public async Task<IActionResult> DeleteReminder(int NoteID)
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
                var res = await this.noteBL.DeleteReminder(UserID, NoteID);
                if (res == true)
                {
                    return this.Ok(new { success = true, status = 200, message = " Note reminder deleted successfully" });
                }
                return this.BadRequest(new { success = true, status = 200, message = "No Reminder found" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("UpdateNoteColour/{NoteID}/{Colour}")]
        public async Task<IActionResult> UpdateColour( int NoteID,string Colour)
        {
            try
            {
                var note = funDoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 200, message = "Provide a correct note" });
                }
                var res=await this.noteBL.UpdateColour(UserID, NoteID,Colour);
                if (res == true)
                {
                    return this.Ok(new { success = true, status = 200, message = " Note colour updated successfully" });
                }
                return this.BadRequest(new { success = true, status = 200, message = "Cannot change colour....note in the trash folder" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllNotesColour")]
        public IActionResult GetAllNote_Colour()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                List<NotesColourModel> note = new List<NotesColourModel>();
                note = this.noteBL.GetAllNote_Colour(UserID);
                return this.Ok(new { success = true, status = 200, noteList = note });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
