using System;

namespace Domain.Tests
{
    public class TestDataGenerator
    {
        public static string NewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
