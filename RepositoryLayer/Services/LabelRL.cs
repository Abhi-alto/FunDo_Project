using RepositoryLayer.Interface;
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
                Label label = new Label();
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
    }
}
