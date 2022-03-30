using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.Controllers
{
    [Route("[controller]")]
    public class ChatController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetChatView()
        {
            return View("Views/Chat/Chat.cshtml");
        }
    }
}
