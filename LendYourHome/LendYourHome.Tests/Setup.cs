namespace LendYourHome.Tests
{
    using AutoMapper;
    using LendYourHome.Application.Infrastructure.Mapping;

    public class Setup
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (!testsInitialized)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                testsInitialized = true;
            }
        }
    }
}
