using System.Collections.Generic;

namespace MedicalAppointmentApp.Models
{
    public class CustomResponse
    {
        private CustomError _error = new CustomError();
        public CustomError Error => _error;
        public bool Success {
            get
            {
                if (_error.Error == null)
                {
                    return true;
                }
                return false;
            } 
        }

        public void AddError(CustomError customError) {
            if (customError != null)
            {
                _error = customError;
            }
        }
    }
}
