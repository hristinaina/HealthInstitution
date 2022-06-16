using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Repositories;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    public class SecretaryReferralService
    {
        private IReferralRepositoryService _referralService;
        private IDoctorRepositoryService _doctorService;
        private IPatientRepositoryService _patientService;

        public SecretaryReferralService()
        {
            _referralService = new ReferralRepositoryService();
            _doctorService = new DoctorRepositoryService();
            _patientService = new PatientRepositoryService();
        }

        public void RemoveReferral(int referralId)
        {
            Referral referral = _referralService.FindByID(referralId);
            Patient patient = _patientService.FindByID(referral.PatientId);

            patient.Record.Referrals.Remove(referral);
            _referralService.GetReferrals().Remove(referral);
        }

        public void UseReferral(int referralId, DateTime datetime)
        {
            Referral referral = _referralService.FindByID(referralId);
            Patient patient = _patientService.FindByID(referral.PatientId);

            bool done = ScheduleAppointment(referral, patient, datetime);
            if (!done)
            {
                throw new Exception("The appointment is not available. Please choose another date or time!");
            }
        }

        private bool ScheduleAppointment(Referral referral, Patient patient, DateTime datetime)
        {
            if (referral.DoctorId != -1)
            {
                Doctor doctor = _doctorService.FindByID(referral.DoctorId);
                if (!ScheduleAppointmentByDoctor(doctor, patient, datetime))
                {
                    return false;
                }
            }
            else
            {
                if (!ScheduleAppointmentBySpecialization(referral.Specialization, patient, datetime))
                {
                    return false;
                }
            }
            return true;
        }

        private bool ScheduleAppointmentBySpecialization(Specialization specialization, Patient patient, DateTime datetime)
        {
            bool done = false;
            bool specializationException = false;
            foreach (Doctor doctor in _doctorService.GetDoctors())
            {
                if (doctor.Specialization == specialization)
                {
                    specializationException = true;
                    done = ScheduleAppointmentByDoctor(doctor, patient, datetime);
                    if (done)
                    {
                        break;
                    }
                }
            }
            if (!specializationException) throw new Exception("There are currently no doctors with selected specialization that work in this hospital.");
            return done;
        }

        private bool ScheduleAppointmentByDoctor(Doctor doctor, Patient patient, DateTime datetime)
        {
            Examination appointment = new(doctor, patient, datetime);
            return new SecretaryScheduleAppointmentService().ScheduleAppointment(appointment, 15);
        }

        public void RemoveReferralsOfDeletedPatients()
        {
            List<Referral> referrals = new List<Referral>(_referralService.GetReferrals().ToArray());
            foreach (Referral referral in referrals)
            {
                Patient patient = _patientService.FindByID(referral.PatientId);
                if (patient.Deleted == true)
                {
                    _referralService.GetReferrals().Remove(referral);
                }
            }
        }

        public List<Referral> SearchMatchingReferrals(string phrase)
        {
            List<Referral> matchingReferrals = new();

            foreach (Referral r in _referralService.GetReferrals())
            {
                Patient patient = _patientService.FindByID(r.PatientId);
                string name = patient.FirstName + " " + patient.LastName;
                if (name.ToLower().Contains(phrase.ToLower()))
                {
                    matchingReferrals.Add(r);
                }
            }
            return matchingReferrals;
        }
    }
}
