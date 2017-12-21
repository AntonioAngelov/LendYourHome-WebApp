namespace LendYourHome.Application.Models.Messages
{
    using System.Collections.Generic;
    using Services.ServiceModels.Messages;

    public class SentMessagesViewModel
    {
        public PageListingModel PageListingData { get; set; }

        public IEnumerable<SentMessageServiceModel> Messages { get; set; }
    }
}
