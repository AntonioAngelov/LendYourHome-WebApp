namespace LendYourHome.Application.Controllers
{
    using System;
    using Common.Constants;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Messages;
    using Models.MessagesViewModels;
    using Services;

    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IMessageService messages;
        private readonly IUserService users;
        private readonly UserManager<User> userManager;

        public MessagesController(IMessageService messages, IUserService users, UserManager<User> userManager)
        {
            this.messages = messages;
            this.users = users;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult New(string id)
        {
            if (!this.users.Exists(id))
            {
                return this.NotFound();
            }

            var recepientUserName = this.users.GetName(id);

            this.TempData[ApplicationConstants.TempDataMessageRecepientIdKey] = id;

            this.TempData[ApplicationConstants.TempDataMessageRecepienUserNameKey] = recepientUserName;
            TempData.Keep(ApplicationConstants.TempDataMessageRecepienUserNameKey);

            return this.View();
        }

        [HttpPost]
        public IActionResult New(MessageCreateViewModel model)
        {
            if (!TempData.ContainsKey(ApplicationConstants.TempDataMessageRecepientIdKey) ||
                !TempData.ContainsKey(ApplicationConstants.TempDataMessageRecepienUserNameKey))
            {
                return this.NotFound();
            }

            var recepientId = TempData[ApplicationConstants.TempDataMessageRecepientIdKey].ToString();

            if (!this.users.Exists(recepientId))
            {
                return this.NotFound();
            }

            this.TempData.Keep(ApplicationConstants.TempDataMessageRecepientIdKey);
            this.TempData.Keep(ApplicationConstants.TempDataMessageRecepienUserNameKey);

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var senderId = this.userManager.GetUserId(this.User);

            this.messages.Create(senderId, recepientId, model.Subject, model.Text);

            TempData[ApplicationConstants.TempDataSuccessMessageKey] =
                $"Message Sent To {TempData[ApplicationConstants.TempDataMessageRecepienUserNameKey]}!";

            return RedirectToAction("Sent", "Messages", new {area = ""});
        }

        [HttpGet]
        public IActionResult Received(int page = 1)
        {
            var userId = this.userManager.GetUserId(this.User);

            var receivedMessages = this.messages.Received(userId, page, ApplicationConstants.MessagesPageListingSize);

            return this.View(new ReceivedMessagesViewModel
            {
                Messages = receivedMessages,
                PageListingData = new PageListingModel
                {
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(this.messages.TotalReceivedByUser(userId) /
                                                   (double)ApplicationConstants.MessagesPageListingSize)
                }
            });
        }

        [HttpGet]
        public IActionResult Sent(int page = 1)
        {
            var userId = this.userManager.GetUserId(this.User);

            var sentMessages = this.messages.Sent(userId, page, ApplicationConstants.MessagesPageListingSize);

            return this.View(new SentMessagesViewModel
            {
                Messages = sentMessages,
                PageListingData = new PageListingModel
                {
                    CurrentPage = page,
                    TotalPages = (int)Math.Ceiling(this.messages.TotalSentByUser(userId) /
                                                   (double)ApplicationConstants.MessagesPageListingSize)
                }
            });
        }

    }
}
