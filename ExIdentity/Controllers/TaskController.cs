using System.Security.Claims;
using ExIdentity.Data;
using ExIdentity.Data.Models;
using ExIdentity.Models.Board;
using ExIdentity.Models.TaskModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExIdentity.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            TaskViewModelCreate model = new TaskViewModelCreate()
            {
                Boards = GetBoards()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TaskViewModelCreate model)
        {
            if (!GetBoards().Any(x => x.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Bord not picked up!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var taskToAdd = new TaskReal()
            {
                Description = model.Description,
                OwnerId = GetUserId(),
                Title = model.Title,
                BoardId = model.BoardId,
                CreatedOn = DateTime.Now
            };

            await _context.Tasks.AddAsync(taskToAdd);

            await _context.SaveChangesAsync();

            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Tasks.FindAsync(id);

            if (model == null)
            {
                return BadRequest();
            }

            var currUser = GetUserId();

            if (currUser != model.OwnerId)
            {
                return BadRequest();
            }

            TaskViewModelCreate modelEdit = new TaskViewModelCreate()
            {
                Boards = GetBoards(),
                Description = model.Description,
                Title = model.Title
            };

            return View(modelEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskViewModelCreate model)
        {
            var dataModel = await _context.Tasks.FindAsync(id);

            if (dataModel == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!GetBoards().Any(x => x.Id == model.BoardId))
            {
                return BadRequest();
            }

            dataModel.BoardId = model.BoardId;
            dataModel.Description = model.Description;
            dataModel.Title = model.Title;

            await _context.SaveChangesAsync();

            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _context.Tasks
                .Where(x => x.Id == id)
                .Select(x => new TaskDetailsViewModel()
                {
                    Owner = x.Owner.UserName,
                    Board = x.Board.Name,
                    CreatedOn = x.CreatedOn,
                    Description = x.Description,
                    Title = x.Title,
                    Id = x.Id
                }).FirstOrDefaultAsync();

            if (model == null)
            {
                return BadRequest();
            }


            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var modelToDelete = await _context.Tasks.FindAsync(id);

            var currUser = GetUserId();

            if (modelToDelete == null)
            {
                return BadRequest();
            }

            if (modelToDelete.OwnerId != currUser)
            {
                return Unauthorized();
            }

            TaskViewModelCreate modelDelete = new TaskViewModelCreate()
            {
                Id = modelToDelete.Id,
                Title = modelToDelete.Title,
                Description = modelToDelete.Description
            };

            return View(modelDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModelCreate model)
        {
            var modelToDelete = await _context.Tasks.FindAsync(model.Id);

            var currUser = GetUserId();

            if (modelToDelete == null)
            {
                return BadRequest();
            }

            if (modelToDelete.OwnerId != currUser)
            {
                return BadRequest();
            }

            _context.Tasks.Remove(modelToDelete);

            await _context.SaveChangesAsync();

            return RedirectToAction("All", "Board");
        }


        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        private IEnumerable<BoardViewModelOption> GetBoards()
        {
            return _context.Boards
                .Select(x => new BoardViewModelOption()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }
    }
}
