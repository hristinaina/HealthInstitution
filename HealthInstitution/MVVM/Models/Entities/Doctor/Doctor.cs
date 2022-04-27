using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Doctor : User
    {
        private Specialization _specialization;
        private List<Examination> _examinations;
        private List<Operation> _operations;
        private double _rating;

        [JsonProperty("Specialization")]
        public Specialization Specialization { get => _specialization; set { _specialization = value; } }
        [JsonProperty("Rating")]
        public double Rating { get => _rating; set { _rating = value; } }

        public Doctor(Specialization specialization = Specialization.NONE)
        {
            _specialization = specialization;
            _examinations = new List<Examination>();
            _operations = new List<Operation>();
        }


        public Specialization GetSpecialization() => _specialization;
        public void SetSpecialization(Specialization specialization) => _specialization = specialization;
        public List<Examination> GetExaminations() => _examinations;
        public void SetExaminations(List<Examination> examinations) => _examinations = examinations;
        public List<Operation> GetOperations() => _operations;
        public void SetOperations(List<Operation> operations) => _operations = operations;
        public double GetRating() => _rating;
        public void SetRating(double rating) => _rating = rating;

        public bool IsAvailable(DateTime dateTime, int durationInMin = 15)
        {

            Appointment interruptingAppointment = FindInterruptingAppointment(dateTime, durationInMin);
            if (interruptingAppointment == null) return true;
            return false;
        }

        private Appointment FindInterruptingAppointment(DateTime dateTime, int durationInMin = 15)
            // returns null if appointment can be reserved
            // else returns appointment that interrupts (scheduled appoint.) - for the next free appointment calculation
        {
            for (int i = 0; i < _examinations.Count(); i++)
            {
                DateTime examinationStart = _examinations[i].GetDateTime();
                DateTime examinationEnd = examinationStart.AddMinutes(15);
                if (DateTime.Compare(_examinations[i].GetDateTime().Date, dateTime.Date) != 0) continue;
                if (DateTime.Compare(dateTime, examinationStart) >= 0 &&
                    DateTime.Compare(dateTime, examinationEnd) < 0) return _examinations[i];  
                if (DateTime.Compare(dateTime.AddMinutes(durationInMin), examinationStart) > 0 &&
                    DateTime.Compare(dateTime.AddMinutes(durationInMin), examinationEnd) <= 0)
                    return _examinations[i];
            }

            for (int i = 0; i < _operations.Count(); i++)
            {
                DateTime operationStart = _operations[i].GetDateTime();
                DateTime operationEnd = operationStart.AddMinutes(_operations[i].GetDurationInMin());
                if (DateTime.Compare(_operations[i].GetDateTime().Date, dateTime.Date) != 0) continue;
                if (DateTime.Compare(dateTime, operationStart) >= 0 &&
                    DateTime.Compare(dateTime, operationEnd) < 0) return _operations[i];
                if (DateTime.Compare(dateTime.AddMinutes(durationInMin), operationStart) > 0 &&
                    DateTime.Compare(dateTime.AddMinutes(durationInMin), operationEnd) <= 0)
                    return _operations[i];
            }

            return null; 
        }

        public List<DateTime> FindFreeTime(DateTime date, int durationInMin = 15) 
        {
            List<DateTime> availableTime = new();
            
            DateTime dateTime = date;
            dateTime = dateTime.AddHours(8);       // date == today, time == 8:00
            DateTime borderTime = date;
            borderTime = borderTime.AddHours(20);  // doctor works till 20 pm

            while (DateTime.Compare(dateTime.AddMinutes(durationInMin), borderTime) <= 0) 
            {
                Appointment interruptingAppointment = FindInterruptingAppointment(dateTime, durationInMin);
                if (interruptingAppointment == null)
                {
                    availableTime.Add(dateTime);
                    dateTime = dateTime.AddMinutes(durationInMin);
                    continue;
                }

                if (interruptingAppointment.GetType().Equals(typeof(Examination)))
                {
                    dateTime = interruptingAppointment.GetDateTime().AddMinutes(15);  // continuing at the end of 
                }                                                                     // interrupting appointment  
                else
                {
                    Operation scheduledOperation = (Operation) interruptingAppointment;
                    dateTime = scheduledOperation.GetDateTime().AddMinutes(scheduledOperation.GetDurationInMin());
                }
            }

            return availableTime;
        }
    }
}
