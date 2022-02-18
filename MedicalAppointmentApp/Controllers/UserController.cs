using MediatR;
using MedicalAppointmentApp.Commands;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using MedicalAppointmentApp.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisteredUsers()
        {
            var userRolesViewModel = await _mediator.Send(new GetRegisteredUsers.Query());
            return View(userRolesViewModel);
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _mediator.Send(new DeleteRegisteredUser.Command { Id = id });
            if (response.Succeeded)
                return RedirectToAction("RegisteredUsers");
            else
                Errors(response);
            return RedirectToAction("RegisteredUsers");
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
