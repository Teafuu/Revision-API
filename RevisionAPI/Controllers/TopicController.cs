using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Revision_API.Data;
using Revision_API.Models;


namespace Revision_API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TopicController : ControllerBase
    {
        private readonly Revision_APIContext _context;

        public TopicController(Revision_APIContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "PostTopic")]
        public void CreateTopic(int userId, string title, string description, DateTime revisionDate)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            if (user is null)
                return;

            var topic = new Topic
            {
                Title = title,
                Description = description,
                RevisionDateTime = revisionDate,
                UserId = user.Id
            };

            _context.Topics.Add(topic);
            _context.SaveChanges();
        }

        [HttpGet(Name = "GetTopics")]
        public ICollection<Topic> GetTopics(int userId)
        {
            return _context.Topics.Where(topic => topic.UserId == userId).ToList();
        }
    }
}
