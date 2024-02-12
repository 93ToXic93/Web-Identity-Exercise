using ExIdentity.Data.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ExIdentity.Models.Board;
using static ExIdentity.Data.DataConstants.TaskValid;

namespace ExIdentity.Models.TaskModels
{
    public class TaskViewModelCreate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(DescriptionMaxLength,MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DisplayName("Board")]
        public int BoardId { get; set; }

        public IEnumerable<BoardViewModelOption> Boards { get; set; } = new List<BoardViewModelOption>();

    }
}
