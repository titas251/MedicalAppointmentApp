using MediatR;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Mediator.Queries;
using MedicalAppointmentApp.Models;
using MedicalAppointmentApp.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> SearchView([FromQuery(Name = "q")] string q)
        {
            int numOfAppointmentsToGet = 10;
            var doctorsViewModel = await _mediator.Send(new GetDoctorsByQuery.Query((q ?? ""), numOfAppointmentsToGet));
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
