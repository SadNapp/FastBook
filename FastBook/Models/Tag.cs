
namespace FastBook.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
