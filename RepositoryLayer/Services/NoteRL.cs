using CommonLayer;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
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
                /*note.isPin=noteModel.isPin;
                note.isReminder=noteModel.isReminder;
                note.isTrash=noteModel.isTrash;
                note.isArchive=noteModel.isArchive;
                note.Reminder=noteModel.Reminder;*/
                fundoContext.Notes.Add(note);
                fundoContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
