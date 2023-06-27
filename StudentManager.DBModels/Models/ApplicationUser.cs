using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StudentManager.DBModels
{
    public class ApplicationUser: IdentityUser
    {
        public string City { get; set; }
    }
}





