using MediatR;
using MedicalAppointmentApp.Mediator.Commands;
using MedicalAppointmentApp.Models;
using MedicalAppointmentApp.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Controllers
{
    [Route("[controller]")]
    public class SpecialityController : Controller
    {
        private readonly IMediator _mediator;

        public SpecialityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateMedicalSpeciality()
        {
            return View();
        }

        [HttpGet("list")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SpecialityList()
        {
            var specialitiesViewModel = await _mediator.Send(new GetMedicalSpecialties.Query());

            var customResponse = TempData.Get<CustomResponse>("CustomResponse");
            if (customResponse != null)
            {
                ViewBag.CustomResponse = customResponse;
            }

            return View(specialitiesViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMedicalSpeciality([FromForm] CreateMedicalSpecialityModel medicalSpecialityModel)
        {
            var response = await _mediator.Send(new CreateMedicalSpeciality.Command 
            {  
                MedicalSpecialityModel = medicalSpecialityModel
            });

            TempData.Put("CustomResponse", response);

            return RedirectToAction("SpecialityList");
        }
    }
}
