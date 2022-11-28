using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using Revision_API.API.Messaging.Interfaces;
using Revision_API.Data;

namespace Revision_API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NotificationsController : ControllerBase
    {
        private readonly Revision_APIContext _context;
        private readonly IMessagingApi _messagingApi;

        public NotificationsController(Revision_APIContext context, IMessagingApi messagingApi)
        {
            _context = context;
            _messagingApi = messagingApi;
        }

        [HttpPost(Name = "SendPushNotification")]
        public async Task<ActionResult> SendPushNotification(int topicId)
        {
            var topic = _context.Topics.FirstOrDefault(x => x.Id == topicId);

            if (topic is null) return BadRequest("Invalid TopicId");

            var request = new Message
            {
                Notification = new Notification
                {
                    Body = topic.Description,
                    Title = topic.Title
                },
                Token = topic.User.DeviceToken
            };

            await _messagingApi.SendNotification(request);
            return Ok();
        }
    }
}