using BuisnessLayer.Interface;
using CommonLayer.Label;
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

        public async Task<bool> Delete_NoteLabel(int UserId, int NoteID)
        {
            try
            {
                return await this.labelRL.Delete_NoteLabel(UserId, NoteID);
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
                return await this.labelRL.Update_NoteLabel(UserId, NoteID, LabelName);
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
                return await this.labelRL.GetLabelByNoteID(UserId, NoteID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LabelModel>> GetAll_LabelsByNoteID(int UserId)
        {
            try
            {
                return await this.labelRL.GetAll_LabelsByNoteID(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
