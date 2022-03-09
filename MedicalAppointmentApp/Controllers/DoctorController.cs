using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Mediator.Commands;
using MedicalAppointmentApp.Mediator.Queries;
using MedicalAppointmentApp.Models;
using MedicalAppointmentApp.Models.ViewModels;
using MedicalAppointmentApp.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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

            var customResponse = TempData.Get<CustomResponse>("CustomResponse");
            if (customResponse != null)
            {
                ViewBag.CustomResponse = customResponse;
            }

            return View(doctorsViewModel);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("add/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAddInstitutionToDoctorView(int id)
        {
            var institutionsViewModel = await _mediator.Send(new GetInstitutions.Query());
            List<ScheduleDetailModel> scheduleDetails = new List<ScheduleDetailModel>();
            for (int i = 0; i < 7; i++)
            {
                var scheduleDetail = new ScheduleDetailModel { Day = (DayOfWeek)i };
                scheduleDetails.Add(scheduleDetail);
            }
            
            CreateInstitutionDoctorViewModel createInstitutionDoctorViewModel = new CreateInstitutionDoctorViewModel()
            {
                DoctorId = id,
                Institutions = institutionsViewModel,
                scheduleDetails = scheduleDetails
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
                InstitutionId = model.InstitutionId,
                scheduleDetails = model.scheduleDetails
            });

            TempData.Put("CustomResponse", response);

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

            TempData.Put("CustomResponse", response);

            return RedirectToAction("DoctorList");
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetDoctorByQuery ([FromQuery(Name = "q")] string query)
        {
            var doctorsViewModel = await _mediator.Send(new GetDoctorsByQuery.Query(query));
            return View("DoctorList", doctorsViewModel);
        }
    }
}
