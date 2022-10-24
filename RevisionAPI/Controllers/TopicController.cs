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

        [HttpPost(Name = "PostTopic")] 
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

        [HttpPost(Name = "ReviseTopic")]
        public void Revise(SendReviseRequest request)
        {
            var topic = _context.Topics.FirstOrDefault(x => x.Id == request.Id);

            if (topic is null || topic.UserId != request.UserId)
                return;
            topic.ReminderCount++;
            topic.LastRevisedDateTime = DateTime.Now;
            topic.RevisionDateTime.AddDays(7);
            _context.SaveChanges();
        }

        [HttpPatch(Name = "PatchTopic")] 
        public void PatchTopic([FromBody] PatchTopicRequest request)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);

            if (user is null)
                return;

            var topic = _context.Topics.Find(request.Id);

            if (topic is null || topic.UserId != request.UserId)
                return;
            
            topic.Color = request.Color;
            topic.Description = request.Description;
            topic.Title = request.Title;
            topic.RevisionDateTime = request.RevisionDateTime;
            
            _context.Topics.Update(topic);
            _context.SaveChanges();
        }

        [HttpGet(Name = "GetTopics")]
        public GetTopicsResponse GetTopics(int id)
        {
            return new GetTopicsResponse {Topics = _context.Topics.Where(topic => topic.UserId == id).ToList()};
        }
    }
}
