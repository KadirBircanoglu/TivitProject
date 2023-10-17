using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TivitProject_EL.IdentityModels;
using TivitProject_EL.ViewModels;

namespace TivitProject_PL.Models
{
    public class TivitIndexViewModel
    {
        public long Id { get; set; }

        public DateTime InsertedDate { get; set; }

        [StringLength(400, MinimumLength = 2)]
        public string Tivit { get; set; }
        public string UserId { get; set; } //FK

        public bool IsDefaultTivit { get; set; }

        public bool IsDeleted { get; set; }

        public AppUser? AppUser { get; set; }

        public List<IFormFile>? SelectedPictures { get; set; }

        public List<TivitPhotoDTO>? TivitPhotos { get; set; }

    }
}
