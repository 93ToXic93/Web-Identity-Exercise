using ExIdentity.Data;
using ExIdentity.Models.Board;
using ExIdentity.Models.TaskModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ExIdentity.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardController(ApplicationDbContext context)
        {
           _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var tasks = await _context.Boards
                .AsNoTracking()
                .Select(x => new BoardViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Tasks = x.Tasks.Select(t => new TaskRealViewModel()
                    {
                        Description = t.Description,
                        Id = t.Id,
                        Owner = t.Owner.UserName,
                        Title = t.Title

                    })

                })
                .ToListAsync();
                

            return View(tasks);
        }
    }
}
