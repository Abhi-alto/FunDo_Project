using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface ILabelBL
    {
        public Task AddLabelNote(int UserId, int NoteID, string LabelName);
    }
}
