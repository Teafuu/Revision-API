using FirebaseAdmin.Messaging;
using Revision_API.API.Messaging.Interfaces;
using Revision_API.Data;
using Revision_API.Models;

namespace Revision_API.Services
{
    public class ReminderService : IHostedService, IDisposable
    {
        private readonly Revision_APIContext _context;
        private readonly IMessagingApi _messagingApi;
        private readonly ILogger<ReminderService> _logger;
        private Timer? _timer;

        public ReminderService(ILogger<ReminderService> logger, IServiceProvider serviceProvider)
        {
            var provider = serviceProvider.CreateScope().ServiceProvider;
            _context = provider.GetRequiredService<Revision_APIContext>();
            _messagingApi = provider.GetRequiredService<IMessagingApi>();
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(0, message:"Starting Reminder Service");

            _timer = new Timer(Remind, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(0, message: "Stopping Reminder Service");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void Remind(object? state)
        {
            foreach (var topic in _context.Topics)
            {
                if (topic.NextPossibleRemindDateTime >= DateTime.Now) // Already been reminded
                    continue;
                
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
            _logger.LogInformation(1, message: $"Sending Reminder to: {topic.User.Name} in topic: {topic.Title}");

            _messagingApi.SendNotification(new Message
            {
                Notification = new Notification
                {
                    Title = $"{topic.Title}",
                    Body = $"{topic.Description}"
                },
                Token = topic.User.DeviceToken
            });

            topic.NextPossibleRemindDateTime = DateTime.Now.AddDays(1);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
