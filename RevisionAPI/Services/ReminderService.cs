using Revision_API.Data;
using Revision_API.Models;

namespace Revision_API.Services
{
    public class ReminderService 
    {
        private readonly Revision_APIContext _context;

        public ReminderService(Revision_APIContext context)
        {
            _context = context;
        }   
            
        public void Remind()
        {
            foreach (var topic in _context.Topics)
            {
                if (topic.ReminderCount == 0 && topic.RevisionDateTime <= DateTime.Now)
                {
                    SendReminder(topic);
                }
                if (topic.ReminderCount is > 0 and <= 2 && topic.RevisionDateTime.AddDays(7) <= DateTime.Now)
                {
                    SendReminder(topic);
                }
                else if(topic.ReminderCount > 2 && topic.RevisionDateTime.AddDays(15) <= DateTime.Now)
                {
                    SendReminder(topic);
                }
            }
        }

        public void SendReminder(Topic topic)
        {
            //idek how to do this
        }
    }
}
