using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    public class Label
    {
        public String LabelName { get; set; }
        public int NoteID { get; set; }
        public Note Note { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
