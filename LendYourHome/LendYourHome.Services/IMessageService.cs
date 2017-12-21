namespace LendYourHome.Services
{
    using System.Collections.Generic;
    using ServiceModels.Messages;

    public interface IMessageService
    {
        void Create(string senderId, string recepientId, string subject, string text);

        IEnumerable<ReceivedMessageServiceModel> Received(string userId, int pageNumber,
            int pageSize);

        IEnumerable<SentMessageServiceModel> Sent(string userId, int pageNumber,
            int pageSize);

        int TotalReceivedByUser(string userId);

        int TotalSentByUser(string userId);
    }
}
