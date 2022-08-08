namespace Revision_API.Models
{
    public class Birthday
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthdayDateTime { get; set; }
        public ICollection<Image> Images { get; set; }

    }
}
