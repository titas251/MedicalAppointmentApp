using MediatR;
using MedicalAppointmentApp.Mediator.Commands;
using MedicalAppointmentApp.Mediator.Queries;
using MedicalAppointmentApp.Models;
using MedicalAppointmentApp.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Controllers
{
    [Route("[controller]")]
    public class InstitutionController : Controller
    {
        private readonly IMediator _mediator;

        public InstitutionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateInstitution()
        {
            return View();
        }

        [HttpGet("list")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InstitutionList(
            [FromQuery(Name = "pageNumber")] int? pageNumber,
            [FromQuery(Name = "pageSize")] int? pageSize)
        {
            ViewBag.PageNumber = pageNumber ?? 1;
            ViewBag.PageSize = pageSize ?? 10;

            int institutionCount = await _mediator.Send(new GetInstitutionCount.Query());
            if (institutionCount == 0) ViewBag.HasNextPage = true;
            else ViewBag.HasNextPage = Math.Ceiling((double)institutionCount / (double)(pageSize ?? 10)) == (pageNumber ?? 1);

            var institutionsViewModel = await _mediator.Send(new GetInstitutionsPaging.Query(pageNumber ?? 1, pageSize ?? 10));

            var customResponse = TempData.Get<CustomResponse>("CustomResponse");
            if (customResponse != null)
            {
                ViewBag.CustomResponse = customResponse;
            }

            return View(institutionsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateInstitution([FromForm] CreateInstitutionModel institutionModel)
        {
            var response = await _mediator.Send(new CreateInstitution.Command
            {
                InstitutionModel = institutionModel
            });

            TempData.Put("CustomResponse", response);

            return RedirectToAction("InstitutionList");
        }

        [HttpPost("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInstitution(int id)
        {
            var response = await _mediator.Send(new DeleteInstitution.Command { Id = id });

            TempData.Put("CustomResponse", response);

            return RedirectToAction("InstitutionList");
        }
    }
}
