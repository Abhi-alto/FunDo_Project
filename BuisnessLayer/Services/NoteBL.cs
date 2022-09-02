using BuisnessLayer.Interface;
using CommonLayer;
using CommonLayer.Notes;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
