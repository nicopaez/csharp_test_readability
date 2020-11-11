using System;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void WhenCreatedIsNotBlocked()
        {
            var customer = new Customer("john", "doe", Guid.NewGuid().ToString());
            Assert.That(customer.IsBlocked(), Is.False);
        }

        [Test]
        public void WhenSetBlockedThenIsBlocked()
        {
            var customer = new Customer("john", "doe", Guid.NewGuid().ToString());
            customer.SetBlocked();
            Assert.That(customer.IsBlocked(), Is.True);
        }
    }
}
