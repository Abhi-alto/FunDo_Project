using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Collaborator
{
    public class CollaboratorModel
    {
        public int CollaboratorID { get; set; }
        public int UserId { get; set; }
        public int NoteID { get; set; }
        public string CollabEmail { get; set; }
    }
}
