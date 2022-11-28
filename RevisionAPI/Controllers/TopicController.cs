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
        public ActionResult CreateTopic(CreateTopicRequest request)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);

                if (user is null)
                    BadRequest("User not found");

                var topic = new Topic
                {
                    Title = request.Title,
                    Description = request.Description,
                    RevisionDateTime = request.RevisionDateTime,
                    Color = request.Color,
                    User = user
                };

                _context.Topics.Add(topic);
                _context.SaveChanges();

                return Ok();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "ReviseTopic")]
        public ActionResult Revise(SendReviseRequest request)
        {
            try
            {
                var topic = _context.Topics.FirstOrDefault(x => x.Id == request.Id);

                if (topic is null || topic.User.Id != request.UserId)
                    return BadRequest("Invalid user id");

                topic.ReminderCount++;
                topic.LastRevisedDateTime = DateTime.Now;
                topic.RevisionDateTime = topic.RevisionDateTime.AddDays(7);

                _context.SaveChanges();
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch(Name = "PatchTopic")] 
        public ActionResult PatchTopic([FromBody] PatchTopicRequest request)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);

                if (user is null)
                    return BadRequest("User not found");

                var topic = _context.Topics.Find(request.Id);

                if (topic is null || topic.User.Id != request.UserId)
                    return BadRequest("Invalid User");

                topic.Color = request.Color;
                topic.Description = request.Description;
                topic.Title = request.Title;
                topic.RevisionDateTime = request.RevisionDateTime;

                _context.Topics.Update(topic);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet(Name = "GetTopics")]
        public ActionResult<GetTopicsResponse> GetTopics(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == id);

                if (user is null)
                    return BadRequest("User not found");
                var topics = _context.Topics.Where(x => x.User.Id == id).Select(x => TopicDto.MapFromDbObject(x));
                
                return Ok(new GetTopicsResponse{Topics = topics.ToList()});
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
