
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace KikisCom.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? firstName { get; set; }
        public string? secondName { get; set; }
        public string? lastName { get; set; }
        public DateTime registerDate { get; set; } = DateTime.Now;
        public bool isDeleted { get; set; } = false;
        public DateTime? deletedDate { get; set; }
    }
}
