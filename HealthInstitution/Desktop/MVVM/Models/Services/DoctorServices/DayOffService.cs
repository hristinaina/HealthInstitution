using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;

namespace HealthInstitution.Core.Services
{
    public class DayOffService
    {
        private readonly DayOffRepository _dayOffRepository;

        public DayOffService()
        {
            _dayOffRepository = Institution.Instance().DayOffRepository;
        }

        public void AcceptRequest(int id)
        {
            DayOff dayOff = _dayOffRepository.FindByID(id);
            dayOff.State = State.ACCEPTED;
            dayOff.Doctor.Notifications.Add("Your request for days ofF from date " + dayOff.BeginDate.ToString() + " to date " + dayOff.EndDate.ToString() + " has been ACCEPTED!");
        }

        public void RejectRequest(int id)
        {
            DayOff dayOff = _dayOffRepository.FindByID(id);
            dayOff.State = State.REJECTED;
            dayOff.Doctor.Notifications.Add("Your request for days ofF from date " + dayOff.BeginDate.ToString() + " to date " + dayOff.EndDate.ToString() + " has been REJECTED!");
        }

        public List<DayOff> FindByDoctorID(int id)
        {
            List<DayOff> daysOffRequests = new();

            foreach (DayOff dayOff in _dayOffRepository.DaysOff)
            {
                if (dayOff.Doctor.ID == id)
                {
                    if (dayOff.BeginDate >= DateTime.Now) daysOffRequests.Add(dayOff);
                }
            }

            return daysOffRequests;
        }

        public bool CheckAvailability(DayOff dayOff, Doctor doctor)
        {
            DoctorService service = new DoctorService(doctor);
            int durationInMin = (int)(dayOff.EndDate - dayOff.BeginDate).TotalMinutes;
            if (service.IsAvailable(dayOff.BeginDate, durationInMin)) return true;
            return false;
        }

        public bool ApplyForDaysOff(DayOff dayOff, Doctor doctor)
        {
            if (!CheckAvailability(dayOff, doctor)) return false;
            Institution.Instance().DayOffRepository.DaysOff.Add(dayOff);
            DoctorDaysOff doctorDaysOff = new DoctorDaysOff(doctor.ID, dayOff.ID);
            Institution.Instance().DoctorDaysOffRepository.DoctorDaysOff.Add(doctorDaysOff);
            return true;
        }
    }
}
