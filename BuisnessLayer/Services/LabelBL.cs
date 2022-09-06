using BuisnessLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class LabelBL:ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public async Task AddLabelNote(int UserId, int NoteID, string LabelName)
        {
            try
            {
                await this.labelRL.AddLabelNote(UserId, NoteID, LabelName);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
