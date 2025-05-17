public interface IChatBotService
{
    Task<string> GetResponseAsync(string prompt);
}
