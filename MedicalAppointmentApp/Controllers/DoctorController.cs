using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Mediator.Commands;
using MedicalAppointmentApp.Models;
using MedicalAppointmentApp.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Controllers
{
    [Route("[controller]")]
    public class DoctorController : Controller
    {
        private readonly IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateDoctor()
        {
            return View();
        }
        [HttpGet("doctorList")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DoctorList()
        {
            var doctorsViewModel = await _mediator.Send(new GetDoctors.Query());
            return View(doctorsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDoctor(CreateDoctorModel doctorModel)
        {
            var response = await _mediator.Send(new CreateDoctor.Command
            {
                DoctorModel = doctorModel
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
