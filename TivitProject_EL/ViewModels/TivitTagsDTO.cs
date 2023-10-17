using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TivitProject_EL.Entities;

namespace TivitProject_EL.ViewModels
{
    public class TivitTagsDTO
    {
        public int Id { get; set; }

        public DateTime InsertedDate { get; set; }

        public string Name { get; set; }

        public long TivitId { get; set; } //FK

        public bool IsDeleted { get; set; }

        public UserTivit? UserTivit { get; set; }
    }
}
