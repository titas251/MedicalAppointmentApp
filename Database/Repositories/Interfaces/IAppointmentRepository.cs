using DAL.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task DeleteAsync(int appointmentId);
        Task<bool> GetAppointmentsByUserIdAndAppointmentIdAsync(string userId, int appointmentId);
        Task<int> GetCountAsync();
        Task<int> GetCountByUserIdAsync(string userId);
        Task<List<Appointment>> GetAllWithPagingAsync(int pageNumber, int pageSize);
        Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<List<Appointment>> GetAppointmentsByUserIdAsync(string userId, int pageNumber, int pageSize);
    }
}
