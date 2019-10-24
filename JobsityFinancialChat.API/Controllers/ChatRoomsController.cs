using AutoMapper;
using JobsityFinancialChat.Domain.Models.API.Chatroom;
using JobsityFinancialChat.Domain.Models.DB;
using JobsityFinancialChat.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobsityFinancialChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatroomsController : ControllerBase
    {
        private readonly IMapper _mapper;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly IDatabaseProvider _databaseProvider;

        public ChatroomsController(UserManager<ApplicationUser> userManager, IDatabaseProvider databaseProvider, IMapper mapper)
        {
            _mapper = mapper;
            _databaseProvider = databaseProvider;
            _userManager = userManager;
        }

        // GET api/chatroom
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var chatrooms = await _databaseProvider.GetChatrooms();

            //var response = (new StockService()).GetStock("aapl.us");

            return Ok(_mapper.Map<IEnumerable<ChatroomModel>>(chatrooms));
        }

        // POST api/chatroom
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ChatroomModel model)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            var chatroom = await _databaseProvider.CreateChatroom(user.Id, model.Name);

            return Ok(chatroom);
        }

        // POST api/chatroom
        [HttpPost]
        [Route("join")]
        public async Task<IActionResult> Join([FromBody] Guid chatroomId)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            var chatroom = await _databaseProvider.Join(user, chatroomId);

            return Ok(chatroom);
        }
    }
}
