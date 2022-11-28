namespace Revision_API.Models.Dto.Response
{
    public class TopicDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RevisionDateTime { get; set; }
        public DateTime LastRevisedDateTime { get; set; }
        public int ReminderCount { get; set; }
        public string Color { get; set; }
        public int UserId { get; set; }

        public static TopicDto MapFromDbObject(Topic dbTopic) => new TopicDto
        {
            Id = dbTopic.Id,
            Title = dbTopic.Title,
            Description = dbTopic.Description,
            RevisionDateTime = dbTopic.RevisionDateTime,
            LastRevisedDateTime = dbTopic.LastRevisedDateTime,
            ReminderCount = dbTopic.ReminderCount,
            Color = dbTopic.Color,
            UserId = dbTopic.User.Id
        };
    }
}
