using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ExIdentity.Data.DataConstants.TaskValid;

namespace ExIdentity.Data.Models
{
    [Comment("TaskReal Table")]
    public class TaskReal
    {
        [Key]
        [Comment("TaskReal identification")]
        public int Id { get; set; }

        [Required]
        [Comment("TaskReal title")]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Comment("TaskReal Description")]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Comment("TaskReal creation time")]
        public DateTime? CreatedOn { get; set; }

        [Comment("Board identification")]
        public int BoardId { get; set; }

        [ForeignKey(nameof(BoardId))]
        public Board? Board { get; set; } = null!;

        [Required]
        [Comment("Owner identification")]

        public string OwnerId { get; set; } = string.Empty;

        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

    }
}
