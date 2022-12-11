using Flunt.Notifications;

namespace Minimal.Api.Entities.Contracts.Base
{
    public class BaseContract<TEntity> : Notifiable<Notification> where TEntity : class
    {
        public TEntity Entity { get; protected set; }
    }
}
