using BuisnessLayer.Interface;
using CommonLayer;
using CommonLayer.Notes;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Note = RepositoryLayer.Services.Entities.Note;

namespace BuisnessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public void AddNote(NoteModel noteModel, int UserId)
        {
            try
            {
                this.noteRL.AddNote(noteModel,UserId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ArchiveNote(int UserId, int NoteID)
        {
            try
            {
                return await this.noteRL.ArchiveNote(UserId,NoteID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteNote(int UserId, int NoteID)
        {
            try
            {
                return this.noteRL.DeleteNote(UserId, NoteID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<NoteResponseModel> GetAllNotes(int UserId)
        {
            try
            {
                return this.noteRL.GetAllNotes(UserId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Note GetNote(int UserId, int NoteID)
        {
            try
            {
                return this.noteRL.GetNote(UserId, NoteID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PinNote(int UserId, int NoteID)
        {
            try
            {
                return await this.noteRL.PinNote(UserId, NoteID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> Trash_Note(int UserId, int NoteID)
        {
            try
            {
                return await this.noteRL.Trash_Note(UserId, NoteID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNote(UpdateNoteModel updateNoteModel, int UserId, int NoteID)
        {
            try
            {
                this.noteRL.UpdateNote(updateNoteModel, UserId, NoteID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> ReminderNote(int UserId, int NoteID, DateTime reminder)
        {
            try
            {
                return await this.noteRL.ReminderNote(UserId, NoteID,reminder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
