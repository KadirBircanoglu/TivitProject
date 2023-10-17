using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TivitProject_EL.IdentityModels
{
    public class AppUser : IdentityUser
    {
        [StringLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string Surname { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(250, MinimumLength = 2)]
        public string? About { get; set; }

        public string? ProfilePicture { get; set; }

    }
}
