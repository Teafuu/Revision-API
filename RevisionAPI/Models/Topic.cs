namespace Revision_API.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RevisionDateTime { get; set; }
        public int ReminderCount { get; set; }
        public int UserId { get; set; }
    }
}
