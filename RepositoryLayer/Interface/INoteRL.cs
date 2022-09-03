using CommonLayer;
using CommonLayer.Notes;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        void AddNote(NoteModel noteModel,int UserId);
        public void UpdateNote(UpdateNoteModel updateNoteModel,int UserId,int NoteID);
        public bool DeleteNote(int UserId,int NoteID);
        public Note GetNote(int UserId, int NoteID);
        public List<NoteResponseModel> GetAllNotes(int UserId);
    }
}
