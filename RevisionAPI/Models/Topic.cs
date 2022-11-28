using System.Drawing;

namespace Revision_API.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RevisionDateTime { get; set; }
        public DateTime LastRevisedDateTime { get; set; }
        public DateTime NextPossibleRemindDateTime { get; set; }
        public int ReminderCount { get; set; }
        public string Color { get; set; }
        public virtual User User { get; set; }
    }
}
