using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Notes
{
    public class UpdateNoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public bool isPin { get; set; }
        public bool isReminder { get; set; }
        public bool isArchive { get; set; }
        public bool isTrash { get; set; }
        public DateTime Reminder { get; set; }
    }
}
