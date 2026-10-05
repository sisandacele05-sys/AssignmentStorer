using AssignmentStorer.Data;
using AssignmentStorer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentStorer.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AssignmentController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // Show all assignments
        public async Task<IActionResult> Index()
        {
            var assignments = await _context.Assignments
                .OrderByDescending(a => a.UploadDate)
                .ToListAsync();

            return View(assignments);
        }

        // Show assignment details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .FirstOrDefaultAsync(a => a.AssignmentId == id);

            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // Show create form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Save assignment and file
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Assignment assignment,
            IFormFile? assignmentFile)
        {
            if (ModelState.IsValid)
            {
                if (assignmentFile != null && assignmentFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(
                        _environment.WebRootPath,
                        "uploads");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName =
                        Guid.NewGuid().ToString() +
                        Path.GetExtension(assignmentFile.FileName);

                    var filePath = Path.Combine(
                        uploadsFolder,
                        uniqueFileName);

                    using (var fileStream = new FileStream(
                        filePath,
                        FileMode.Create))
                    {
                        await assignmentFile.CopyToAsync(fileStream);
                    }

                    assignment.FileName = assignmentFile.FileName;
                    assignment.FilePath = "/uploads/" + uniqueFileName;
                }

                assignment.UploadDate = DateTime.Now;

                _context.Assignments.Add(assignment);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(assignment);
        }

        // Delete assignment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var assignment = await _context.Assignments
                .FindAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }

            // Delete physical file if it exists
            if (!string.IsNullOrEmpty(assignment.FilePath))
            {
                var physicalPath = Path.Combine(
                    _environment.WebRootPath,
                    assignment.FilePath.TrimStart('/'));

                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }

            _context.Assignments.Remove(assignment);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}