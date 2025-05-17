using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using TALLERCHATBOX.Models;

public class ChatController : Controller
{
    private readonly Dictionary<string, IChatBotService> _providers;

    public ChatController()
    {
        _providers = new Dictionary<string, IChatBotService>
        {
            { "gemini", new GeminiChatBotService() },
            { "chatgpt", new OpenAIChatBotService() }
        };
    }

    public ActionResult Index()
    {
        return View(new ChatViewModel());
    }

    [HttpPost]
    public async Task<ActionResult> Index(ChatViewModel model)
    {
        if (_providers.ContainsKey(model.Proveedor))
        {
            var servicio = _providers[model.Proveedor];
            var respuesta = await servicio.GetResponseAsync(model.Pregunta);
            model.Respuesta = respuesta;
        }
        else
        {
            model.Respuesta = "Proveedor no encontrado.";
        }

        return View(model);
    }
}
