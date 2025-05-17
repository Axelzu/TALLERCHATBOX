public class OpenAIChatBotService : IChatBotService
{
    private readonly HttpClient _httpClient;

    public OpenAIChatBotService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetResponseAsync(string prompt)
    {
        // Simulación de ChatGPT free. Puedes sustituirlo con Groq u otro si no tienes API.
        return await Task.FromResult($"Respuesta simulada de ChatGPT: {prompt}");
    }
}
