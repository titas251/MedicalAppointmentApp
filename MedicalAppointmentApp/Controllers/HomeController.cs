using MediatR;
using MiddleProject.Queries;
using MiddleProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var customResponse = TempData.Get<CustomResponse>("CustomResponse");
            if (customResponse != null)
            {
                ViewBag.CustomResponse = customResponse;
            }

            return View();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchView(
            [FromQuery(Name = "q")] string q,
            [FromQuery(Name = "currentFilter")] string currentFilter,
            [FromQuery(Name = "pageNumber")] int? pageNumber,
            [FromQuery(Name = "pageSize")] int? pageSize)
        {
            int numOfAppointmentsToGet = 5;

            if (q != null) pageNumber = 1;
            else q = currentFilter;

            ViewBag.CurrentFilter = q;
            ViewBag.PageNumber = pageNumber ?? 1;
            ViewBag.PageSize = pageSize ?? 10;
            ViewBag.DoctorCount = await _mediator.Send(new GetDoctorCountByQuery.Query(q ?? ""));
            var doctorsViewModel = await _mediator.Send(new GetDoctorsByQuery.Query(q ?? "", numOfAppointmentsToGet, pageNumber ?? 1, pageSize ?? 10));
            
            return View("Search", doctorsViewModel);
        }

        [Authorize]
        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
