using System;
using System.ComponentModel.DataAnnotations;

namespace AssignmentStorer.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }

        [Required]
        [Display(Name = "Assignment Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Module { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}