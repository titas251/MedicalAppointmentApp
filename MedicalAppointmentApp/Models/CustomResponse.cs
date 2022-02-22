using System.Collections.Generic;

namespace MedicalAppointmentApp.Models
{
    public class CustomResponse
    {
        private bool _success { get; set; } = true;
        private List<CustomError> _errors = new List<CustomError>();

        public bool Success => _success;
        public IEnumerable<CustomError> Errors => _errors;

        public void AddErrors(CustomError customError) {
            if (customError != null)
            {
                _success = false;
                _errors.Add(customError);
            }
        }
    }
}
