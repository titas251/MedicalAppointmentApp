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
            return View(specialitiesViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMedicalSpeciality(CreateMedicalSpecialityModel medicalSpecialityModel)
        {
            var response = await _mediator.Send(new CreateMedicalSpeciality.Command 
            {  
                MedicalSpecialityModel = medicalSpecialityModel
            });
            if (!response.Success)
                Errors(response);

            return RedirectToAction("SpecialityList");
        }

        private void Errors(CustomResponse response)
        {
            foreach (CustomError error in response.Errors)
                ModelState.AddModelError(error.Error, error.Message);
        }
    }
}
