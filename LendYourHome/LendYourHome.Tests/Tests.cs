namespace LendYourHome.Tests
{
    using AutoMapper;
    using LendYourHome.Application.Infrastructure.Mapping;

    public class Tests
    {
        private static bool testsInitialized;

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
