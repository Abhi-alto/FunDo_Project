using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface INoteBL
    {
        void AddNote(NoteModel noteModel, int UserId);
    }
}
