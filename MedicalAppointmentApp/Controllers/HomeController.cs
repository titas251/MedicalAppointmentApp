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
        public async Task<IActionResult> Index([FromQuery(Name = "q")] string q)
        {
            var doctorsViewModel = new List<GetDoctorsWithNextAppointments>();
            
            if (!String.IsNullOrEmpty(q))
            {
                doctorsViewModel = await _mediator.Send(new GetDoctorsByQuery.Query(q));
            }

            return View(doctorsViewModel);
        }

        [Authorize]
        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
