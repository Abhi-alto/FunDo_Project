using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
   public class User
    {
            [Key]
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime modifyDate { get; set; }
        }
}
