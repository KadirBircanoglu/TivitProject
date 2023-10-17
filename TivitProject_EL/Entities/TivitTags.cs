using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TivitProject_EL.Entities
{
    [Table("TivitTags")]

    public class TivitTags : BaseNumeric<int>
    {
        public string Name { get; set; }

        public long TivitId { get; set; } //FK

        public bool IsDeleted { get; set; }

        [ForeignKey("TivitId")]
        public virtual UserTivit UserTivit { get; set; }
    }
}
