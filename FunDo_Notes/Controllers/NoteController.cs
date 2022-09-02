﻿using BuisnessLayer.Interface;
using CommonLayer;
using CommonLayer.Notes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Linq;

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
        public IActionResult UpdateNote(UpdateNoteModel updateNoteModel,int NoteID)
        {
            var note = funDoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);
            if(note==null)
            {
                return this.BadRequest(new { success = false, status = 200, message = "Provide a correct note" });
            }
            this.noteBL.UpdateNote(updateNoteModel, UserID, NoteID);
            return this.Ok(new { success = true, status = 200, message = "Note Added successfully" });
        }
        [Authorize]
        [HttpDelete("DeleteNote/{NoteID}")]
        public IActionResult DeleteNote(int NoteID)
        {
            var deleteNote = funDoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);
            if(deleteNote==null)
            {
                return this.BadRequest(new { success = false, status = 200, message = "Provide an existing note" });
            }
            bool answer= this.noteBL.DeleteNote(UserID, NoteID);
            return this.Ok(new { success = true, status = 200, message = "Note deleted successfully" });
        }
        [Authorize]
        [HttpGet("GetNote/{NoteID}")]
        public IActionResult GetNote(int NoteID)
        {
            Note specificNote=new Note();
            var getNote = funDoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);
            if(getNote==null)
            {
                return this.BadRequest(new { success = false, status = 200, message = "Provide an existing Note ID" });
            }
            specificNote=this.noteBL.GetNote(UserID, NoteID);
            return this.Ok(new { success = true, status = 200, note=specificNote });
        }
    }
}
