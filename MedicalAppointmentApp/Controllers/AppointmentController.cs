using MediatR;
using MedicalAppointmentApp.Mediator.Commands;
using MedicalAppointmentApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Controllers
{
    [Route("[controller]")]
    public class AppointmentController : Controller
    {
        private readonly IMediator _mediator;
        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Authorize(Roles = "Basic")]
        public IActionResult CreateAppointmentView(string doctorId)
        {
            var appointmentViewModel = new CreateAppointmentModel()
            {
                DoctorId = Int32.Parse(doctorId),
                ApplicationUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            return View("CreateAppointment", appointmentViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> CreateAppointment([FromForm] CreateAppointmentModel appointmentModel)
        {
            var response = await _mediator.Send(new CreateAppointment.Command
            {
                AppointmentModel = appointmentModel
            });
            if (!response.Success)
                Errors(response);
            
            //TODO: route to user appointments list
            return RedirectToAction("Index", "Home");
        }
        
        private void Errors(CustomResponse response)
        {
            foreach (CustomError error in response.Errors)
                ModelState.AddModelError(error.Error, error.Message);
        }
    }
}
