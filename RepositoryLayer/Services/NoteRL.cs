 using CommonLayer;
using CommonLayer.Notes;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note = RepositoryLayer.Services.Entities.Note;

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

        public void UpdateNote(UpdateNoteModel updateNoteModel, int UserId, int NoteID)
        {
            try
            {
                var notes = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (notes == null || notes.isTrash == true)
                {
                    notes.Title = updateNoteModel.Title == "string" ? notes.Title : updateNoteModel.Title;
                    notes.Description = updateNoteModel.Description == "string" ? notes.Description : updateNoteModel.Description;
                    notes.Colour = updateNoteModel.Colour == "string" ? notes.Colour : updateNoteModel.Colour;
                    notes.isPin = updateNoteModel.isPin;
                    notes.isReminder = updateNoteModel.isReminder;
                    notes.isArchive = updateNoteModel.isArchive;
                    notes.isTrash = updateNoteModel.isTrash;
                    notes.Reminder = updateNoteModel.Reminder;
                    notes.modifiedDate = DateTime.Now;
                }
                fundoContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public bool DeleteNote(int UserId, int NoteID)
        {
            try
            {
                var deleteNote = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (deleteNote == null)
                {
                    return false;
                }
                fundoContext.Notes.Remove(deleteNote);
                fundoContext.SaveChanges();
                return true;
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
                var user=fundoContext.Users.Where(x => x.UserId == UserId).FirstOrDefault();
                //var note=fundoContext.Notes.Where(x => x.UserId == UserId).ToList(); // using LINQ
                //using LINQ join
                return fundoContext.Users
               .Where(u => u.UserId == UserId)
               .Join(fundoContext.Notes,
               u => u.UserId,
               n => n.UserId,
               (u, n) => new NoteResponseModel
               {
                   NoteID = n.NoteID,
                   UserId = u.UserId,
                   Title = n.Title,
                   Description = n.Description,
                   Colour = n.Colour,
                   Firstname = u.FirstName,
                   Lastname = u.LastName,
                   Email = u.email,
                   CreatedDate = u.CreatedDate,
               }).ToList();
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
                var getNote = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                return getNote;
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
                var note = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if(note == null||note.isTrash==true)
                {
                    return false;
                }
                if (note.isArchive == true)
                {
                    note.isArchive = false;
                }
                else
                {
                    note.isArchive = true;
                }
                await fundoContext.SaveChangesAsync();
                return true;
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
                var note = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (note == null || note.isTrash == true)
                {
                    return false;
                }
                if (note.isPin == true)
                {
                    note.isPin = false;
                }
                else
                {
                    note.isPin = true;
                }
                await fundoContext.SaveChangesAsync();
                return true;
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
                var note = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (note == null )
                {
                    return false;
                }
                if (note.isTrash == true)
                {
                    note.isTrash = false;
                }
                else
                {
                    note.isTrash = true;
                }
                await fundoContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> ReminderNote(int UserId, int NoteID,DateTime reminder)
        {
            try
            {
                var note = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (note == null || note.isTrash == true)
                {
                    return false;
                }
                if(note.isReminder == false)
                {
                    note.isReminder = true;
                    
                }
                note.Reminder = reminder;
                await fundoContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteReminder(int UserId, int NoteID)
        {
            try
            {
                var note = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (note.isTrash == true|| note.isReminder == false)
                {
                    return false;
                }
                note.isReminder = false;
                note.Reminder=default(DateTime);
                await fundoContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateColour(int UserId, int NoteID, string Colour)
        {
            try
            {
                var note = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (note.isTrash == true)
                {
                    return false;
                }
                note.Colour = Colour;
                await fundoContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
