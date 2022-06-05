using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Repositories.References;
using HealthInstitution.Repositories;

namespace HealthInstitution.MVVM.Models.Services
{
    public class ReferralService
    {
        private readonly ReferralRepository _referralRepository;
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;

        public ReferralService()
        {
            _referralRepository = Institution.Instance().ReferralRepository;
            _patientRepository = Institution.Instance().PatientRepository;
            _doctorRepository = Institution.Instance().DoctorRepository;
        }

        public void RemoveReferral(int referralId)
        {
            Referral referral = _referralRepository.FindByID(referralId);
            Patient patient = _patientRepository.FindByID(referral.PatientId);

            patient.Record.Referrals.Remove(referral);
            _referralRepository.Referrals.Remove(referral);
        }

        public void UseReferral(int referralId, DateTime datetime)
        {
            Referral referral = _referralRepository.FindByID(referralId);
            Patient patient = _patientRepository.FindByID(referral.PatientId);

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
                Doctor doctor = _doctorRepository.FindByID(referral.DoctorId);
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
            foreach (Doctor doctor in _doctorRepository.Doctors)
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
            List<Referral> referrals = new List<Referral>(_referralRepository.Referrals.ToArray());
            foreach (Referral referral in referrals)
            {
                Patient patient = _patientRepository.FindByID(referral.PatientId);
                if (patient.Deleted == true)
                {
                    _referralRepository.Referrals.Remove(referral);
                }
            }
        }

        public List<Referral> SearchMatchingReferrals(string phrase)
        {
            List<Referral> matchingReferrals = new();

            foreach (Referral r in _referralRepository.Referrals)
            {
                Patient patient = _patientRepository.FindByID(r.PatientId);
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
