namespace Revision_API.Models.Dto.Request
{
    public class PatchTopicRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RevisionDateTime { get; set; }
        public string Color { get; set; }
        public int UserId { get; set; }
    }
}
