using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class GeminiChatBotService : IChatBotService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "TU_API_KEY_DE_GEMINI";

    public GeminiChatBotService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetResponseAsync(string prompt)
    {
        var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key=" + _apiKey;

        var requestBody = new
        {
            contents = new[] {
                new {
                    parts = new[] {
                        new { text = prompt }
                    }
                }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(result);
        var text = doc.RootElement
                      .GetProperty("candidates")[0]
                      .GetProperty("content")
                      .GetProperty("parts")[0]
                      .GetProperty("text")
                      .GetString();

        return text ?? "No se obtuvo respuesta.";
    }
}
