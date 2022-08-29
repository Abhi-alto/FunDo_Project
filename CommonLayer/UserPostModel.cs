using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class UserPostModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]{1}[A-Za-z]{3,}",ErrorMessage ="Start with a capital letter and must have minimum three letters")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{1}[A-Za-z]{3,}", ErrorMessage = "Start with a capital letter and must have minimum three letters")]
        public string LastName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
