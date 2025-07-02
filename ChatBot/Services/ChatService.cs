using System.Data;
using ChatBot.Model;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ChatBot.Services
{
    public interface IChatService
    {
        Task<ChatMessage> GetBotReplyAsync(string userMessage);
    }
    public class ChatService:IChatService
    {
        private readonly IConfiguration _config;
        public ChatService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<ChatMessage>GetBotReplyAsync(string userMessage)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var parameters = new DynamicParameters();
            parameters.Add("UserMessage", userMessage);
            parameters.Add("@BotReply", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);
            await connection.ExecuteAsync("ChatBotReply", parameters, commandType: CommandType.StoredProcedure);

            var botReply = parameters.Get<string>("@BotReply");

            return new ChatMessage
            {
                BotReply = botReply,
                UserMessage = userMessage
            };
        }
    }
}
