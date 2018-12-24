using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OKRs.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public Guid UserId
        {
            get
            {
                return Guid.Parse(Id);
            }
        }

        public string Name { get; set; }
        public bool Inactive { get; set; }

        public string DisplayName
        {
            get
            {
                return string.IsNullOrEmpty(Name) ? Email : Name;
            }
        }
    }
}
