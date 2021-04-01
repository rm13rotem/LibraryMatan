using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryMatan.Models
{
    public partial class MembershipUser
    {
        public int Id { get; set; }
        
        [Required]
        public string UserNameText { get; set; }
        [Required]
        public string UserIdentityNumber { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

        public string Role { get
            {
                if (UserNameText == "user1")
                    return "Admin";
                else return "User";
            }
        }
        public DateTime CreatedDateTime { get; set; }
    }
}
