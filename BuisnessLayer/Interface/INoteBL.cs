using CommonLayer;
using CommonLayer.Notes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface INoteBL
    {
        void AddNote(NoteModel noteModel, int UserId);
        public void UpdateNote(UpdateNoteModel updateNoteModel, int UserId, int NoteID);
    }
}
