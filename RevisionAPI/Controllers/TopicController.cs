using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Revision_API.Data;
using Revision_API.Models;
using Revision_API.Models.Dto.Request;
using Revision_API.Models.Dto.Response;


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

        [HttpPost(Name = "PostTopic")] //TODO: Remake into  Request and Response Objects.
        public void CreateTopic(CreateTopicRequest request)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);

            if (user is null)
                return;

            var topic = new Topic
            {
                Title = request.Title,
                Description = request.Description,
                RevisionDateTime = request.RevisionDateTime,
                Color = request.Color,
                UserId = user.Id
            };

            _context.Topics.Add(topic);
            _context.SaveChanges();
        }

        [HttpGet(Name = "GetTopics")]
        public GetTopicsResponse GetTopics(int id)
        {
            return new GetTopicsResponse {Topics = _context.Topics.Where(topic => topic.UserId == id).ToList()};
        }
    }
}
