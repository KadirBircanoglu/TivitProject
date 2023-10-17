using System.ComponentModel.DataAnnotations;
using TivitProject_EL.IdentityModels;

namespace TivitProject_PL.Models
{
    public class ProfileViewModel : AppUser
    {
        public IFormFile? SelectedPhoto { get; set; }
    }
}
