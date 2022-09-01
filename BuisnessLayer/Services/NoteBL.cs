using BuisnessLayer.Interface;
using CommonLayer;
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
    }
}
