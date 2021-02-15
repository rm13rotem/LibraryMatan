using System;
using System.Collections.Generic;

namespace LibraryMatan.Models
{
    public partial class MembershipUser
    {
        public int Id { get; set; }
        public string UserNameText { get; set; }
        public string UserIdentityNumber { get; set; }

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
