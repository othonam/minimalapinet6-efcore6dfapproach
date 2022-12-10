using Minimal.Api.DTOs;
using Minimal.Api.Entities.Mappings;

namespace Minimal.Test
{
    public class CostumerTests
    {
        [Fact]
        public void Costumer_Contract_Success_1()
        {
            var contract = new CostumerContract();

            contract.MapTo(new CostumerPost
            {
                FirstName = "Bilbo",
                LastName = "Baggins"
            });

            Assert.True(contract.IsValid);
        }

        [Fact]
        public void Costumer_Contract_Success_2()
        {
            var contract = new CostumerContract();

            contract.MapTo(new CostumerPost
            {
                FirstName = "Gandalf",
                LastName = "The Gray"
            });

            Assert.True(contract.IsValid);
        }

        [Fact]
        public void Costumer_Contract_Success_3()
        {
            var contract = new CostumerContract();

            contract.MapTo(new CostumerPost
            {
                FirstName = "Arthas",
                LastName = "Menethil"
            });

            Assert.True(contract.IsValid);
        }

        [Fact]
        public void Costumer_Contract_Failure_1()
        {
            var contract = new CostumerContract();

            contract.MapTo(new CostumerPost
            {
                FirstName = "TestingStringOverflowTestingStringOverflow",
                LastName = "TestingStringOverflowTestingStringOverflowTestingStringOverflow"
            });

            Assert.False(contract.IsValid);
        }

        [Fact]
        public void Costumer_Contract_Failure_2()
        {
            var contract = new CostumerContract();

            contract.MapTo(new CostumerPost
            {
                FirstName = null,
                LastName = null
            });

            Assert.False(contract.IsValid);
        }

        [Fact]
        public void Costumer_Contract_Failure_3()
        {
            var contract = new CostumerContract();

            contract.MapTo(new CostumerPost
            {
                FirstName = string.Empty,
                LastName = string.Empty
            });

            Assert.False(contract.IsValid);
        }
    }
}