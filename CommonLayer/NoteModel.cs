using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class NoteModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]{1}[A-Za-z]{3,}", ErrorMessage = "Start with a capital letter and must have minimum three letters")]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Colour { get; set; }

    }
}
