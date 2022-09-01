using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    public class Note
    {
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public bool isPin { get; set; }
        public bool isReminder { get; set; }
        public bool isArchive { get; set; }
        public bool isTrash { get; set; }
        public DateTime Reminder { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate{ get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
