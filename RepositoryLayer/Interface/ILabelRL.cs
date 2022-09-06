using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public Task AddLabelNote(int UserId,int NoteID,string LabelName);
    }
}
