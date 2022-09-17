using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    public class Collaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollaboratorID { get; set; }
        public string CollabEmail { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int NoteID { get; set; }
        public Note Note { get; set; }

    }
}
