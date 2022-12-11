using Minimal.Api.DTOs;
using Minimal.Api.Entities.Contracts;

namespace Minimal.Test
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_Contract_Success_1()
        {
            var contract = new CustomerContract();

            contract.MapToEntity(new CustomerPost
            {
                FirstName = "Bilbo",
                LastName = "Baggins"
            });

            Assert.True(contract.IsValid);
        }

        [Fact]
        public void Customer_Contract_Success_2()
        {
            var contract = new CustomerContract();

            contract.MapToEntity(new CustomerPost
            {
                FirstName = "Gandalf",
                LastName = "The Gray"
            });

            Assert.True(contract.IsValid);
        }

        [Fact]
        public void Customer_Contract_Success_3()
        {
            var contract = new CustomerContract();

            contract.MapToEntity(new CustomerPost
            {
                FirstName = "Arthas",
                LastName = "Menethil"
            });

            Assert.True(contract.IsValid);
        }

        [Fact]
        public void Customer_Contract_Failure_1()
        {
            var contract = new CustomerContract();

            contract.MapToEntity(new CustomerPost
            {
                FirstName = "TestingStringOverflowTestingStringOverflow",
                LastName = "TestingStringOverflowTestingStringOverflowTestingStringOverflow"
            });

            Assert.False(contract.IsValid);
        }

        [Fact]
        public void Customer_Contract_Failure_2()
        {
            var contract = new CustomerContract();

            contract.MapToEntity(new CustomerPost
            {
                FirstName = null,
                LastName = null
            });

            Assert.False(contract.IsValid);
        }

        [Fact]
        public void Customer_Contract_Failure_3()
        {
            var contract = new CustomerContract();

            contract.MapToEntity(new CustomerPost
            {
                FirstName = string.Empty,
                LastName = string.Empty
            });

            Assert.False(contract.IsValid);
        }
    }
}