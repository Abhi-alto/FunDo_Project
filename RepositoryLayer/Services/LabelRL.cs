using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
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
    }
}
