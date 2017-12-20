namespace LendYourHome.Tests.Mocks
{
    using System;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class LendYourHomeDbMock
    {
        public static LendYourHomeDbContext New()
        {
            var dbOptions = new DbContextOptionsBuilder<LendYourHomeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LendYourHomeDbContext(dbOptions);
        }
    }
}
