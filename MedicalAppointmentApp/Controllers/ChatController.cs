using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointmentApp.Controllers
{
    [Route("[controller]")]
    public class ChatController : Controller
    {
        public IActionResult GetChatView()
        {
            return View("Views/Chat/Chat.cshtml");
        }
    }
}
