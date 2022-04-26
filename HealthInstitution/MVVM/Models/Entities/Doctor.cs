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

        private static List<DateTime> SortAscending(List<DateTime> list)
        {
            list.Sort((a, b) => a.CompareTo(b));
            return list;
        }

        // returns list with 2 lists, first (0) list is when appoinments starts
        // and second (1) list is when appointments end
        private List<List<DateTime>> GetBusyTime()
        {
            List<List<DateTime>> busyTime = new();
            List<DateTime> examinationsStart = new();
            List<DateTime> examinationsEnd = new();

            foreach (Examination examination in _examinations)
            {
                DateTime startDateTime = examination.GetDateTime();
                DateTime endDateTime = startDateTime.AddMinutes(15);
                examinationsStart.Add(startDateTime);
                examinationsEnd.Add(endDateTime);

            }

            foreach (Operation operation in _operations)
            {
                DateTime startDateTime = operation.GetDateTime();
                DateTime endDateTime = startDateTime.AddMinutes(operation.GetDurationInMin());
                examinationsStart.Add(startDateTime);
                examinationsEnd.Add(endDateTime);

            }

            examinationsStart = SortAscending(examinationsStart);
            examinationsEnd = SortAscending(examinationsEnd);
            busyTime.Add(examinationsStart);
            busyTime.Add(examinationsEnd);
            return busyTime;
        }

        public (bool, DateTime) IsAvailable(List<DateTime> appointmentsStart, List<DateTime> appointmentsEnd,
                                 DateTime dateTime, int durationInMin)
        {
            for (int i = 0; i < appointmentsStart.Count(); i++)
            {
                if (DateTime.Compare(dateTime, appointmentsStart[i]) >= 0 &&
                    DateTime.Compare(dateTime, appointmentsEnd[i]) < 0) return (false, appointmentsEnd[i]);
                if (DateTime.Compare(dateTime.AddMinutes(durationInMin), appointmentsStart[i]) > 0 &&
                    DateTime.Compare(dateTime.AddMinutes(durationInMin), appointmentsEnd[i]) <= 0) 
                    return (false, appointmentsEnd[i]);
            }
            
            return (true, DateTime.Today);
        }



        public List<DateTime> FindFreeTime(int durationInMin = 15)  // TODO : add for other dates, not just for today
        {
            List<List<DateTime>> busyTime = GetBusyTime();
            List<DateTime> availableTime = new();
            
            List<DateTime> appointmentsStart = busyTime[0];
            List<DateTime> appointmentsEnd = busyTime[1];
            
            DateTime dateTime = DateTime.Today;
            dateTime = dateTime.AddHours(7);  // date == today, time == 8:00
            dateTime = dateTime.AddMinutes(45);
            DateTime borderTime = DateTime.Today;
            borderTime = borderTime.AddHours(20);

            while (DateTime.Compare(dateTime.AddMinutes(durationInMin), borderTime) <= 0)
            {
                (bool isAvailable, DateTime newDateTime) = IsAvailable(appointmentsStart, appointmentsEnd, dateTime, durationInMin);
                if (isAvailable)
                {
                    availableTime.Add(dateTime);
                    dateTime = dateTime.AddMinutes(durationInMin);
                }

                else
                {
                    dateTime = newDateTime;
                }
            }

            return availableTime;
        }
    }
}
