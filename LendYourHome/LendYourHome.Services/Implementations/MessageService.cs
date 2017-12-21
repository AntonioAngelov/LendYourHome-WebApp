namespace LendYourHome.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using ServiceModels.Messages;

    public class MessageService : IMessageService
    {
        private readonly LendYourHomeDbContext db;

        public MessageService(LendYourHomeDbContext db)
        {
            this.db = db;
        }


        public void Create(string senderId, string recepientId, string subject, string text)
        {
            var message = new Message
            {
                SenderId = senderId,
                RecipientId = recepientId,
                Subject = subject,
                Text = text,
                SentDate = DateTime.UtcNow
            };

            this.db.Messages.Add(message);
            this.db.SaveChanges();
        }

        public IEnumerable<ReceivedMessageServiceModel> Received(string userId,
            int pageNumber,
            int pageSize)
            => this.db
                .Messages
                .Where(m => m.RecipientId == userId)
                .OrderByDescending(m => m.SentDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ReceivedMessageServiceModel>()
                .ToList();

        public IEnumerable<SentMessageServiceModel> Sent(string userId,
            int pageNumber,
            int pageSize)
            => this.db
                .Messages
                .Where(m => m.SenderId == userId)
                .OrderByDescending(m => m.SentDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<SentMessageServiceModel>()
                .ToList();

        public int TotalReceivedByUser(string userId)
            => this.db
                .Messages
                .Count(m => m.RecipientId == userId);

        public int TotalSentByUser(string userId)
            => this.db
                .Messages
                .Count(m => m.SenderId == userId);
    }
}
