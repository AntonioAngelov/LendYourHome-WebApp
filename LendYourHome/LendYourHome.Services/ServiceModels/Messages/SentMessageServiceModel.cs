namespace LendYourHome.Services.ServiceModels.Messages
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class SentMessageServiceModel : MessageServiceModel, IMapFrom<Message>, IHaveCustomMapping
    {
        public string RecipientId { get; set; }

        public string RecipientUsername { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Message, SentMessageServiceModel>()
                .ForMember(sm => sm.RecipientUsername, cfg => cfg.MapFrom(m => m.Recipient.UserName));
        }
    }
}
