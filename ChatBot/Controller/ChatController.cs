using ChatBot.Model;
using ChatBot.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromHeader] string UserMessage)
        {
            try
            {
                var reply = await _chatService.GetBotReplyAsync(UserMessage);
                return Ok(reply);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}