using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    public class DayOffService : IDaysOffService
    {
        private readonly IDayOffRepository _dayOffRepository;

        public DayOffService()
        {
            _dayOffRepository = Institution.Instance().DayOffRepository;
        }

        public void AcceptRequest(int id)
        {
            DayOff dayOff = new DayOffRepositoryService().FindByID(id);
            dayOff.State = State.ACCEPTED;
            dayOff.Doctor.Notifications.Add("Your request for days ofF from date " + dayOff.StartDate.ToString() + " to date " + dayOff.EndDate.ToString() + " has been ACCEPTED!");
        }

        public void RejectRequest(int id)
        {
            DayOff dayOff = new DayOffRepositoryService().FindByID(id);
            dayOff.State = State.REJECTED;
            dayOff.Doctor.Notifications.Add("Your request for days ofF from date " + dayOff.StartDate.ToString() + " to date " + dayOff.EndDate.ToString() + " has been REJECTED!");
        }

        public List<DayOff> FindByDoctorID(int id)
        {
            List<DayOff> daysOffRequests = new();

            foreach (DayOff dayOff in new DayOffRepositoryService().GetDaysOff())
            {
                if (dayOff.Doctor.ID == id)
                {
                    if (dayOff.StartDate >= DateTime.Now) daysOffRequests.Add(dayOff);
                }
            }

            return daysOffRequests;
        }

        public bool ValidateRequest(DayOff dayOff, Doctor doctor)
        {
            DoctorService service = new DoctorService(doctor);
            int durationInMin = (int)(dayOff.EndDate - dayOff.StartDate).TotalMinutes;
            if (!service.IsAvailable(dayOff.StartDate, durationInMin)) return false;
            if ((dayOff.StartDate - DateTime.Now).TotalDays <= 2) return false;
            if ((dayOff.Emergency is true) && ((dayOff.EndDate - dayOff.StartDate).TotalDays > 5)) 
                return false;
            if (dayOff.StartDate > dayOff.EndDate) return false;
            if (dayOff.StartDate <= DateTime.Now) return false;
            return true;
        }

        public bool ApplyForDaysOff(DayOff dayOff, Doctor doctor)
        {
            if (!ValidateRequest(dayOff, doctor)) return false;
            new DayOffRepositoryService().GetDaysOff().Add(dayOff);
            DoctorDaysOff doctorDaysOff = new DoctorDaysOff(doctor.ID, dayOff.ID);
            new DoctorDaysOffRepositoryService().GetDoctorDaysOff().Add(doctorDaysOff);
            return true;
        }
    }
}
