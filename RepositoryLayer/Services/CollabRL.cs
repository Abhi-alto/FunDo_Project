using CommonLayer.Collaborator;
using CommonLayer.Label;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        FunDoContext funDoContext;
        private IConfiguration _config;
        public CollabRL(FunDoContext funDoContext, IConfiguration _config)
        {
            this.funDoContext = funDoContext;
            this._config = _config;
        }

        public async Task<bool> AddCollaborator(int UserId, int NoteID, string CollabEmail)
        {
            try
            {
                if((funDoContext.Collaborators.Where(e => e.CollabEmail==CollabEmail && e.NoteID==NoteID))!=null) //Not to save same collaborator email again in the same note
                {
                    return false;
                }
                var USER = funDoContext.Users.Where(u => u.UserId == UserId).FirstOrDefault();
                var Notes = funDoContext.Notes.Where(n => n.NoteID == NoteID).FirstOrDefault();
                Collaborator collaborator = new Collaborator();
                collaborator.User = USER;
                collaborator.Note = Notes;
                collaborator.UserId = UserId;
                collaborator.NoteID = NoteID;
                collaborator.CollabEmail = CollabEmail;
                funDoContext.Collaborators.Add(collaborator);
                await funDoContext.SaveChangesAsync();
                return true;
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
                var Collab = funDoContext.Collaborators.Where(c => c.CollaboratorID == CollaboratorID).FirstOrDefault();
                if (Collab == null)
                {
                    return false;
                }
                funDoContext.Collaborators.Remove(Collab);
                await funDoContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<CollaboratorModel>> GetCollabs_ByNoteID(int UserId, int NoteID)
        {
            try
            {
                  var Collab = this.funDoContext.Collaborators.FirstOrDefault(x => x.UserId == UserId);
                var result = await (from user in funDoContext.Users
                                      join notes in funDoContext.Notes on user.UserId equals UserId //where notes.NoteId == NoteId
                                      join collaborators in funDoContext.Collaborators on notes.NoteID equals collaborators.NoteID
                                      where collaborators.UserId == UserId && collaborators.NoteID == NoteID
                                      select new CollaboratorModel
                                      {
                                          CollaboratorID = collaborators.CollaboratorID,
                                           UserId = UserId,
                                           NoteID = notes.NoteID,
                                           CollabEmail = collaborators.CollabEmail,
                                      }).ToListAsync();
                    return result;
            }
            catch (Exception ex)
            {
                 throw ex;
            }
        }

        public async Task<List<CollaboratorModel>> GetCollabs_ByUserID(int UserId)
        {
            try
            {
                var result = await(from user in funDoContext.Users
                                   join notes in funDoContext.Notes on user.UserId equals UserId 
                                   join collaborators in funDoContext.Collaborators on notes.NoteID equals collaborators.NoteID
                                   where collaborators.UserId == UserId
                                   select new CollaboratorModel
                                   {
                                       CollaboratorID = collaborators.CollaboratorID,
                                       UserId = UserId,
                                       NoteID = notes.NoteID,
                                       CollabEmail = collaborators.CollabEmail,
                                   }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

