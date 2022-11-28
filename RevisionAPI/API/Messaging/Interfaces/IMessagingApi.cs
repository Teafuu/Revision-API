using FirebaseAdmin.Messaging;

namespace Revision_API.API.Messaging.Interfaces
{
    public interface IMessagingApi
    {
        Task SendNotification(Message request);
    }
}
