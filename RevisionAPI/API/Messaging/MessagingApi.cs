using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Revision_API.API.Messaging.Interfaces;

namespace Revision_API.API.Messaging
{
    public class MessagingApi : IMessagingApi
    {
        private readonly ILogger<MessagingApi> _logger;
        private readonly FirebaseMessaging _client;

        public MessagingApi(ILogger<MessagingApi> logger)
        {
            _logger = logger;
            if (FirebaseMessaging.DefaultInstance is null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile("FirebaseCredentials.json")
                });
            }
            _client = FirebaseMessaging.DefaultInstance;
        }

        public async Task SendNotification(Message request)
        {
            try
            {
                await _client.SendAsync(request);
            }
            catch (Exception e)
            {
                _logger.LogCritical(0, e, "Failed sending notification");
            }
        }
    }
}
