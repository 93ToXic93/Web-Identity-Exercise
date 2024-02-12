using System.ComponentModel.DataAnnotations;
using static ExIdentity.Data.DataConstants.TaskValid;

namespace ExIdentity.Models.TaskModels
{
    public class TaskRealViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(DescriptionMaxLength,MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Owner { get; set; } = string.Empty;

    }
}
