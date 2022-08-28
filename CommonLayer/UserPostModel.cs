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
        [RegularExpression(@"^[A-Za-z0-9]{3,}([\.\-\+][A-Za-z0-9]{3,})?[@][a-zA-Z0-9]{1,}[.][a-zA-Z]{2,}([.][a-zA-Z]{2,})?$", ErrorMessage = "Email not at par with the naming convention")]
        public string email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[$])(?=.*[0-9])[A-Za-z0-9$#@_]{8,}$",ErrorMessage ="Should have atleast an Upper case, a lower case, a digit and a special character")]
        public string password { get; set; }
    }
}
