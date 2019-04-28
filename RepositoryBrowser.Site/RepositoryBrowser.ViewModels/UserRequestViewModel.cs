using System.ComponentModel.DataAnnotations;

namespace RepositoryBrowser.ViewModels
{
    public class UserRequestViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
