namespace LendYourHome.Services.ServiceModels.Messages
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    public class ReceivedMessageServiceModel : MessageServiceModel, IMapFrom<Message>, IHaveCustomMapping
    {
        public string SenderId { get; set; }

        public string SenderUsername { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Message, ReceivedMessageServiceModel>()
                .ForMember(rm => rm.SenderUsername, cfg => cfg.MapFrom(m => m.Sender.UserName));
        }
    }
}
