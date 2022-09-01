using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class NoteModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Colour { get; set; }
       /* public bool isPin { get; set; }
        public bool isReminder { get; set; }
        public bool isArchive { get; set; }
        public bool isTrash { get; set; }
        public DateTime Reminder{ get; set; }*/
    }
}
