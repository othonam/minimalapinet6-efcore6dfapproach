using Flunt.Notifications;
using Flunt.Validations;
using Minimal.Api.DTOs;

namespace Minimal.Api.Entities.Mappings
{
    public class CostumerContract : Notifiable<Notification>
    {
        public Costumer? MapTo(CostumerPost post)
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(post.FirstName, $"{nameof(post.FirstName)} must be filled.")
                .IsLowerOrEqualsThan(
                    post.FirstName != null? 
                    post.FirstName.Length: 0, 35, $"{nameof(post.FirstName)} must contains 35 max length.")
                .IsNotNullOrEmpty(post.LastName, $"{nameof(post.LastName)} must be filled.")
                .IsLowerOrEqualsThan(
                    post.LastName != null ? 
                    post.LastName.Length : 0, 75, $"{nameof(post.LastName)}  must contains 35 max length.")
                );

            if (IsValid)
                return new Costumer
                {
                    Id = Guid.NewGuid(),
                    FirstName = post.FirstName,
                    LastName = post.LastName
                };
            else 
                return null;
        }

        public Costumer? MapTo(CostumerPut put)
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsNotNull(put.Id, $"{nameof(put.Id)} must be filled.")
                .IsNotNullOrEmpty(put.FirstName, $"{nameof(put.FirstName)} must be filled.")
                .IsLowerOrEqualsThan(
                    put.FirstName != null ? 
                    put.FirstName.Length : 0, 35, $"{nameof(put.FirstName)} must contains 35 max length.")
                .IsNotNullOrEmpty(put.LastName, $"{nameof(put.LastName)} must be filled.")
                .IsLowerOrEqualsThan(
                    put.LastName != null ? 
                    put.LastName.Length : 0, 75, $"{nameof(put.LastName)}  must contains 35 max length.")
                );

            if (IsValid)
                return new Costumer
                {
                    Id = put.Id,
                    FirstName = put.FirstName,
                    LastName = put.LastName
                };
            else
                return null;
        }
    }
}
