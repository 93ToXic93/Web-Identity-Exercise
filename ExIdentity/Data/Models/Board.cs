using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static ExIdentity.Data.DataConstants.BoardValid;

namespace ExIdentity.Data.Models
{
    [Comment("TaskReal board")]
    public class Board
    {
        [Key]
        [Comment("Board identification")]
        public int Id { get; set; }

        [Required]
        [Comment("Board name")]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<TaskReal> Tasks { get; set; } = new List<TaskReal>();

    }
}
