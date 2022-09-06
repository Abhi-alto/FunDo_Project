using CommonLayer;
using CommonLayer.Notes;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface INoteBL
    {
        void AddNote(NoteModel noteModel, int UserId);
        public void UpdateNote(UpdateNoteModel updateNoteModel, int UserId, int NoteID);
        public bool DeleteNote(int UserId,int NoteID);
        public Note GetNote(int UserId, int NoteID);
        public List<NoteResponseModel> GetAllNotes(int UserId);
        Task<bool> ArchiveNote(int UserId, int NoteID);
        Task<bool> PinNote(int UserId, int NoteID);
        Task<bool> Trash_Note(int UserId, int NoteID);
        Task<bool> ReminderNote(int UserId, int NoteID, DateTime reminder);
        Task<bool> DeleteReminder(int UserId, int NoteID);
        Task<bool> UpdateColour(int UserId, int NoteID, string Colour);
    }
}
