using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Mediator.Commands;
using MedicalAppointmentApp.Models;
using MedicalAppointmentApp.Models.ViewModels;
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
        [HttpGet("list")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DoctorList()
        {
            var doctorsViewModel = await _mediator.Send(new GetDoctors.Query());
            return View(doctorsViewModel);
        }

        [HttpGet("createInstitutionDoctor/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCreateInstitutionDoctorView(int id)
        {
            var institutionsViewModel = await _mediator.Send(new GetInstitutions.Query());
            CreateInstitutionDoctorViewModel createInstitutionDoctorViewModel = new CreateInstitutionDoctorViewModel()
            {
                DoctorId = id,
                Institutions = institutionsViewModel
            };
            return View("CreateInstitutionDoctor", createInstitutionDoctorViewModel);
        }

        [HttpPost("createInstitutionDoctor")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateInstitutionDoctor(CreateInstitutionDoctorViewModel model)
        {
            CreateInstitutionDoctorModel _model = new CreateInstitutionDoctorModel
            {
                DoctorId = model.DoctorId,
                InstitutionId = model.InstitutionId
            };
            var response = await _mediator.Send(new CreateInstitutionDoctor.Command
            {
                InstitutionDoctorModel = _model
            });
            if (!response.Success)
                Errors(response);
            return RedirectToAction("DoctorList");
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

            return RedirectToAction("DoctorList");
        }

        private void Errors(CustomResponse response)
        {
            foreach (CustomError error in response.Errors)
                ModelState.AddModelError(error.Error, error.Message);
        }
    }
}
