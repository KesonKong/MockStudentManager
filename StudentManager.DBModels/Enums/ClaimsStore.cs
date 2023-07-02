using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.DBModels
{
    public class ClaimsStore
    {
        public static List<Claim> GetClaims = new List<Claim>() {
            new Claim("Create Role",""),
            new Claim("Edit Role",""),
            new Claim("Delete Role",""),
            new Claim("Edit Student","")
        };
    }
}
