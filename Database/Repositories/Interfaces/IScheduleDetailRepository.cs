using DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IScheduleDetailRepository : IGenericRepository<ScheduleDetail>
    {
        Task<List<ScheduleDetail>> GetDoctorScheduleAsync(int doctorId, string address, DateTime currentDate);
        void AddWithoutSaving(ScheduleDetail scheduleDetail);
    }
}
