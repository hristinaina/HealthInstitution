using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Repositories;

namespace HealthInstitution.MVVM.Models.Services.DoctorServices
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
            dayOff.State = Models.Enumerations.State.ACCEPTED;
            dayOff.Doctor.Notifications.Add("Your request for days ofF from date " + dayOff.BeginDate.ToString() + " to date " + dayOff.EndDate.ToString() + " has been ACCEPTED!");
        }

        public void RejectRequest(int id)
        {
            DayOff dayOff = _dayOffRepository.FindByID(id);
            dayOff.State = Models.Enumerations.State.REJECTED;
            dayOff.Doctor.Notifications.Add("Your request for days ofF from date " + dayOff.BeginDate.ToString() + " to date " + dayOff.EndDate.ToString() + " has been REJECTED!");
        }
    }
}
