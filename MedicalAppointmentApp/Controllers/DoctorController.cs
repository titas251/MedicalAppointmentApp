using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Mediator.Commands;
using MedicalAppointmentApp.Mediator.Queries;
using MedicalAppointmentApp.Models;
using MedicalAppointmentApp.Models.ViewModels;
using MedicalAppointmentApp.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("create")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateDoctor()
        {
            return View();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task<IActionResult> Doctor(int id)
        {
            var doctorViewModel = await _mediator.Send(new GetDoctorById.Query(id));
            return View(doctorViewModel);
        }
        [HttpGet("list")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DoctorList()
        {
            var doctorsViewModel = await _mediator.Send(new GetDoctors.Query());
            return View(doctorsViewModel);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("appointment")]
        public IActionResult CreateAppointmentView(string doctorId, string institutionId)
        {
            var appointmentViewModel = new CreateAppointmentModel() { DoctorId = Int32.Parse(doctorId),
            //reik ir current user id paduot
            };
            return View("CreateAppointment", appointmentViewModel);
        }
        [HttpPost("appointment/create")]
        public async Task<IActionResult> CreateAppointment([FromForm] CreateAppointmentModel appointmentModel)
        {
            var response = await _mediator.Send(new CreateAppointment.Command
            {
                AppointmentModel = appointmentModel
            });
            if (!response.Success)
                Errors(response);

            return RedirectToAction("DoctorList");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("add/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAddInstitutionToDoctorView(int id)
        {
            var institutionsViewModel = await _mediator.Send(new GetInstitutions.Query());
            CreateInstitutionDoctorViewModel createInstitutionDoctorViewModel = new CreateInstitutionDoctorViewModel()
            {
                DoctorId = id,
                Institutions = institutionsViewModel
            };
            return View("AddInstitutionToDoctor", createInstitutionDoctorViewModel);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddInstitutionToDoctor(CreateInstitutionDoctorViewModel model)
        {
            var response = await _mediator.Send(new AddInstitutionToDoctor.Command
            {
                DoctorId = model.DoctorId,
                InstitutionId = model.InstitutionId
            });
            if (!response.Success)
                Errors(response);
            return RedirectToAction("DoctorList");
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDoctor([FromForm] CreateDoctorModel doctorModel)
        {
            var response = await _mediator.Send(new CreateDoctor.Command
            {
                DoctorModel = doctorModel
            });
            if (!response.Success)
                Errors(response);

            return RedirectToAction("DoctorList");
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetDoctorByQuery ([FromQuery(Name = "q")] string query)
        {
            var doctorsViewModel = await _mediator.Send(new GetDoctorsByQuery.Query(query));
            return View("DoctorList", doctorsViewModel);
        }

        private void Errors(CustomResponse response)
        {
            foreach (CustomError error in response.Errors)
                ModelState.AddModelError(error.Error, error.Message);
        }
    }
}
