using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExIdentity.Models.Board
{
    public class BoardViewModelOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
