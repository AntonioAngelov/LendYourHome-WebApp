namespace LendYourHome.Services.ServiceModels.Messages
{
    using System;

    public abstract class MessageServiceModel
    {
        public string Subject { get; set; }
        
        public string Text { get; set; }

        public DateTime SentDate { get; set; }
    }
}
