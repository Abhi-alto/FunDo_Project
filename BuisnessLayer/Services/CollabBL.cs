using BuisnessLayer.Interface;
using CommonLayer;
using CommonLayer.Collaborator;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class CollabBL:ICollabBL
    {
        ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        public async Task<bool> AddCollaborator(int UserId, int NoteID, string CollabEmail)
        {
            try
            {
                return await this.collabRL.AddCollaborator(UserId,NoteID,CollabEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> RemoveCollaborator(int UserId, int NoteID, int CollaboratorID)
        {
            try
            {
                return await this.collabRL.RemoveCollaborator(UserId, NoteID, CollaboratorID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<CollaboratorModel>> GetCollabs_ByNoteID(int UserId, int NoteID)
        {
            try
            {
                return await this.collabRL.GetCollabs_ByNoteID(UserId, NoteID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CollaboratorModel>> GetCollabs_ByUserID(int UserId)
        {
            try
            {
                return await this.collabRL.GetCollabs_ByUserID(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
