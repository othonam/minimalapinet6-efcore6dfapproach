using Flunt.Notifications;
using Flunt.Validations;
using Minimal.Api.DTOs;
using Minimal.Api.Entities.Contracts.Base;

namespace Minimal.Api.Entities.Contracts
{
    public class CustomerContract : BaseContract<Customer>
    {
        private const int FIRSTNAMEMAXLENGTH = 35;
        private const int LASTNAMEMAXLENGTH = 75;

        public void MapToEntity(CustomerPost post)
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(post.FirstName, $"{nameof(post.FirstName)} must be filled.")
                .IsNotNullOrEmpty(post.LastName, $"{nameof(post.LastName)} must be filled."));

            if (post.FirstName != null)
                AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsLowerThan(post.FirstName.Length, FIRSTNAMEMAXLENGTH, $"{nameof(post.FirstName)} must contains 35 max length."));

            if (post.LastName != null)
                AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsLowerThan(post.LastName.Length, LASTNAMEMAXLENGTH, $"{nameof(post.LastName)} must contains 35 max length."));

            if (IsValid)
                Entity = new Customer
                {
                    Id = Guid.NewGuid(),
                    FirstName = post.FirstName,
                    LastName = post.LastName
                };
        }

        public void MapToEntity(CustomerPut put)
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
                Entity = new Customer
                {
                    Id = put.Id,
                    FirstName = put.FirstName,
                    LastName = put.LastName
                };
        }
    }
}
