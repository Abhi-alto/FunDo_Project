using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Label
{
    public class LabelModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]{1}[A-Za-z]{3,}", ErrorMessage = "Start with a capital letter and must have minimum three letters")]
        public string LabelName { get; set; }
        public int UserId { get; set; }
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string Colour { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
