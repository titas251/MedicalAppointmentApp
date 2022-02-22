using MediatR;
using MedicalAppointmentApp.Mediator.Commands;
using MedicalAppointmentApp.Models;
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
        public IActionResult CreateMedicalSpeciality()
        {
            return View();
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

            return View("~/Views/Home/Index.cshtml");
        }

        private void Errors(CustomResponse response)
        {
            foreach (CustomError error in response.Errors)
                ModelState.AddModelError(error.Error, error.Message);
        }
    }
}
