using MVCDropDownProject.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDropDownProject.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UFirstName { get; set; }
        public string ULastName { get; set; }
        public string Mobile { get; set; }
        public string email { get; set; }
        public Nullable<int> CID { get; set; }
        public Nullable<int> SID { get; set; }

        public string CName { get; set; }
        public string SName { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
    }
}