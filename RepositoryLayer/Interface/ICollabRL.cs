using CommonLayer.Collaborator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public Task<bool> AddCollaborator(int UserId, int NoteID, string CollabEmail);
        public Task<bool> RemoveCollaborator(int UserId, int NoteID,int CollaboratorID);
        public Task<List<CollaboratorModel>> GetCollabs_ByNoteID(int UserId,int NoteID);
        public Task<List<CollaboratorModel>> GetCollabs_ByUserID(int UserId);
    }
}
