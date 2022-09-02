using CommonLayer;
using CommonLayer.Notes;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL:INoteRL
    {
        FunDoContext fundoContext;
        public NoteRL(FunDoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }
        public void AddNote(NoteModel noteModel,int UserId)
        {
            try
            {
                Note note=new Note();
                note.UserId = UserId;
                note.Title= noteModel.Title;
                note.Description= noteModel.Description;
                note.Colour= noteModel.Colour;
                note.createdDate = DateTime.Now;
                note.modifiedDate = DateTime.Now;
                fundoContext.Notes.Add(note);
                fundoContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteNote(int UserId, int NoteID)
        {
            var deleteNote = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
            if(deleteNote == null)
            {
                return false;
            }
            fundoContext.Notes.Remove(deleteNote);
            fundoContext.SaveChanges();
            return true;
        }

        public void UpdateNote(UpdateNoteModel updateNoteModel, int UserId, int NoteID)
        {
            try
            {
                var notes = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                notes.Title = updateNoteModel.Title == "string" ? notes.Title : updateNoteModel.Title;
                notes.Description = updateNoteModel.Description == "string" ? notes.Description : updateNoteModel.Description;
                notes.Colour = updateNoteModel.Colour == "string" ? notes.Colour : updateNoteModel.Colour;
                notes.isPin = updateNoteModel.isPin;
                notes.isReminder = updateNoteModel.isReminder;
                notes.isArchive = updateNoteModel.isArchive;
                notes.isTrash = updateNoteModel.isTrash;
                notes.Reminder = updateNoteModel.Reminder;
                notes.modifiedDate = DateTime.Now;
                fundoContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}
