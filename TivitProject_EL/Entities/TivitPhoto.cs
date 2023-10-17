using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TivitProject_EL.Entities
{
    [Table("TivitPhotos")]

    public class TivitPhoto : BaseNumeric<int>
    {
        public long TivitId { get; set; } // FK

        public string MediaPath { get; set; }

        [ForeignKey("TivitId")]
        public virtual UserTivit UserTivit { get; set; }
    }
}
