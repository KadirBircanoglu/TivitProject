using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TivitProject_EL.IdentityModels;

namespace TivitProject_EL.ViewModels
{
    public class UserTivitDTO
    {
        public long Id { get; set; }
        public DateTime InsertedDate { get; set; }

        public string Tivit { get; set; }
        public string UserId { get; set; } //FK

        public bool IsDefaultTivit { get; set; }

        public bool IsDeleted { get; set; }

        public AppUser? AppUser { get; set; }

    }
}
