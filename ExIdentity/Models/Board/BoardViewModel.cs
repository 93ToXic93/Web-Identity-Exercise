using System.ComponentModel.DataAnnotations;
using ExIdentity.Models.TaskModels;
using static ExIdentity.Data.DataConstants.BoardValid; 

namespace ExIdentity.Models.Board
{
    public class BoardViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "The {0} should be between {2} and {1} characters")]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<TaskRealViewModel> Tasks { get; set; } = new List<TaskRealViewModel>();

    }
}
