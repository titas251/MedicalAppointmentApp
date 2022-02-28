using MediatR;
using MedicalAppointmentApp.Mediator.Commands;
using MedicalAppointmentApp.Mediator.Queries;
using MedicalAppointmentApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet("{userId}")]
        [Authorize(Roles = "Basic")]
        public async Task<List<GetAppointmentModel>> GetAppointmentsByUserId(string userId)
        {
            var doctorViewModel = await _mediator.Send(new GetAppointmentsByUserId.Query(userId));

            return doctorViewModel;
        }

        [HttpPost("delete/{id}")]
        [Authorize(Roles = "Basic")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var response = await _mediator.Send(new DeleteAppointmentId.Command { Id = id });
            if (!response.Success)
                Errors(response);

            //change to user appointments
            return RedirectToAction("Index", "Home");
        }

        private void Errors(CustomResponse response)
        {
            foreach (CustomError error in response.Errors)
                ModelState.AddModelError(error.Error, error.Message);
        }
    }
}
