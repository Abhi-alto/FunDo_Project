using CommonLayer.Label;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public Task AddLabelNote(int UserId,int NoteID,string LabelName);
        public Task<bool> Update_NoteLabel(int UserId, int NoteID, string LabelName);
        public Task<bool> Delete_NoteLabel(int UserId, int NoteID);
        public Task<List<LabelModel>> GetLabelByNoteID(int UserId, int NoteID);
        public Task<List<LabelModel>> GetAll_LabelsByNoteID(int UserId);
    }
}
