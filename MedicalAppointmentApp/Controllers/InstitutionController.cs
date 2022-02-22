using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
