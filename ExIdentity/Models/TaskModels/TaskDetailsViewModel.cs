using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExIdentity.Models.TaskModels
{
    public class TaskDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime? CreatedOn { get; set; }

        public string Board { get; set; } = null!;

        public string Owner { get; set; } = string.Empty;

        public int BoardId { get; set; }

    }
}
