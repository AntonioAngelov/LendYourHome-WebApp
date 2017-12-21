namespace LendYourHome.Application.Models.Messages
{
    using System.Collections.Generic;
    using Services.ServiceModels.Messages;

    public class ReceivedMessagesViewModel
    {
        public PageListingModel PageListingData { get; set; }

        public IEnumerable<ReceivedMessageServiceModel> Messages { get; set; }
    }
}
