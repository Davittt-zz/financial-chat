using AutoMapper;
using JobsityFinancialChat.API.Controllers;
using JobsityFinancialChat.API.Hubs;
using JobsityFinancialChat.Domain.Models.API.Chatroom;
using JobsityFinancialChat.Domain.Models.DB;
using JobsityFinancialChat.Providers;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobsityFinancialChat.API.Test
{
    public class TestChatroomController
    {
        [Fact]
        public async Task TestDataTypeAfterGetChatroms()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var mockStore = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockStore.Object, null, null, null, null, null, null, null, null);

            var databseProvider = new Mock<IDatabaseProvider>();
            databseProvider.Setup(repo => repo.GetChatrooms()).ReturnsAsync(GetTestChatrooms());

            var controller = new ChatroomsController(mockUserManager.Object, databseProvider.Object, mapper.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.IsType<List<ChatroomModel>>(result);
        }

        [Fact]
        public async Task TestDataTypeAfterGetMessages()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var mockStore = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockStore.Object, null, null, null, null, null, null, null, null);

            var databseProvider = new Mock<IDatabaseProvider>();
            databseProvider.Setup(repo => repo.GetMessages(It.IsAny<Guid>())).ReturnsAsync(GetMessages());

            var controller = new ChatroomsController(mockUserManager.Object, databseProvider.Object, mapper.Object);

            // Act
            var result = await controller.Get(It.IsAny<Guid>());

            // Assert
            Assert.IsType<List<Message>>(result);
        }

        private List<Chatroom> GetTestChatrooms()
        {
            var chatrooms = new List<Chatroom>();
            chatrooms.Add(new Chatroom()
            {
                Id = new Guid(),
                Name = "Chatroom One"
            });
            chatrooms.Add(new Chatroom()
            {
                Id = new Guid(),
                Name = "Chatroom Two"
            });
            return chatrooms;
        }

        private List<Message> GetMessages()
        {
            var messages = new List<Message>();
            messages.Add(new Message()
            {
                ChatroomId = new Guid(),
                Text = "test1"
            });
            messages.Add(new Message()
            {
                ChatroomId = new Guid(),
                Text = "test2"
            });
            return messages;
        }

        
    }
}

