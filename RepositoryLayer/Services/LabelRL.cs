using CommonLayer.Label;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
//using RepositoryLayer.Migrations;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class LabelRL:ILabelRL
    {
        FunDoContext fundoContext;
        public LabelRL(FunDoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public async Task AddLabelNote(int UserId, int NoteID, string LabelName)
        {
            try
            {
                var user = fundoContext.Users.Where(x => x.UserId == UserId).FirstOrDefault();
                var note=fundoContext.Notes.Where(x=>x.NoteID==NoteID && x.UserId == UserId).FirstOrDefault();
                Label label = new Label();
                label.User = user;
                label.Note = note;
                label.UserId = UserId;
                label.NoteID = NoteID;
                label.LabelName = LabelName;
                fundoContext.Add(label);
                await fundoContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete_NoteLabel(int UserId, int NoteID)
        {
            try
            {
                var note = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (note.isTrash == true || note == null)
                {
                    return false;
                }
                var label = fundoContext.Labels.Where(x => x.NoteID == NoteID).FirstOrDefault();
                fundoContext.Labels.Remove(label);
                await fundoContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> Update_NoteLabel(int UserId, int NoteID, string LabelName)
        {
            try
            {
                var note = fundoContext.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (note.isTrash == true||note==null)
                {
                    return false;
                }
                var label=fundoContext.Labels.Where(x => x.NoteID == NoteID).FirstOrDefault();
                label.LabelName=LabelName;
                await fundoContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<LabelModel>> GetLabelByNoteID(int UserId, int NoteID)
        {
            try
            {
                var label = this.fundoContext.Labels.FirstOrDefault(x => x.UserId == UserId);
                var result = await (from user in fundoContext.Users
                                    join notes in fundoContext.Notes on user.UserId equals UserId //where notes.NoteId == NoteId
                                    join labels in fundoContext.Labels on notes.NoteID equals labels.NoteID
                                    where labels.NoteID == NoteID && labels.UserId == UserId
                                    select new LabelModel
                                    {
                                        UserId = UserId,
                                        NoteId = notes.NoteID,
                                        LabelName = labels.LabelName,
                                        Title = notes.Title,
                                        FirstName = user.FirstName,
                                        LastName = user.LastName,
                                        email = user.email,
                                        Description = notes.Description,
                                        Colour = notes.Colour,
                                        CreatedDate=labels.User.CreatedDate
                                    }).ToListAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
