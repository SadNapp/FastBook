using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FastBook.Models
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; } // PascalCase

        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
  
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public string Category { get; set; } = "Default";
        public int Priority { get; set; } = 1;
        public bool IsPinned { get; set; } = false;

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; } = 250;
        public double Height { get; set; } = 250;

        public string BackgroundColor { get; set; } = "#FFFEA1";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
