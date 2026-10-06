using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentStorer.Controllers
{
    public class AssignmentController : Controller
    {
        private static readonly List<Assignment> assignments = new();

        public IActionResult Index()
        {
            return View(assignments);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(
            string title,
            string subject,
            string description,
            IFormFile file)
        {
            string fileName = "";

            if (file != null && file.Length > 0)
            {
                fileName = Path.GetFileName(file.FileName);

                string uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads");

                Directory.CreateDirectory(uploadsFolder);

                string filePath = Path.Combine(
                    uploadsFolder,
                    fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            assignments.Add(new Assignment
            {
                Title = title,
                Subject = subject,
                Description = description,
                FileName = fileName
            });

            return RedirectToAction("Index");
        }

        // VIEW FILE
        public IActionResult ViewFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound();
            }

            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            string contentType = "application/octet-stream";

            string extension = Path.GetExtension(fileName).ToLower();

            if (extension == ".pdf")
                contentType = "application/pdf";
            else if (extension == ".jpg" || extension == ".jpeg")
                contentType = "image/jpeg";
            else if (extension == ".png")
                contentType = "image/png";

            return PhysicalFile(filePath, contentType);
        }

        // DOWNLOAD FILE
        public IActionResult Download(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound();
            }

            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return PhysicalFile(
                filePath,
                "application/octet-stream",
                fileName);
        }

        // DELETE ASSIGNMENT
        [HttpPost]
        public IActionResult Delete(string fileName)
        {
            var assignment = assignments.FirstOrDefault(
                a => a.FileName == fileName);

            if (assignment != null)
            {
                assignments.Remove(assignment);
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            return RedirectToAction("Index");
        }
    }

    public class Assignment
    {
        public string Title { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Description { get; set; } = "";
        public string FileName { get; set; } = "";
    }
}