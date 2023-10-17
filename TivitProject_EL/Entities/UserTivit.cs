using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TivitProject_EL.IdentityModels;

namespace TivitProject_EL.Entities
{
    [Table("UserTivits")]

    public class UserTivit : BaseNumeric<long>
    {
        [StringLength(400, MinimumLength = 2)]
        public string Tivit { get; set; }
        public string UserId { get; set; } //FK --> AppUser'ın pk'sı string

        public bool IsDefaultTivit { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

    }
}
