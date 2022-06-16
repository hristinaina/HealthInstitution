using HealthInstitution.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class ExaminationRepositoryService : IExaminationRepositoryService
    {
        IExaminationRepository _repository;

        public ExaminationRepositoryService()
        {
            _repository = Institution.Instance().ExaminationRepository;
        }

        public void Add(Examination examination)
        {
            _repository.Add(examination);
        }

        public void DeleteByPatientID(int patientId)
        {
            _repository.DeleteByPatientID(patientId);
        }

        public Appointment FindAppointment(Doctor doctor, Patient patient, DateTime oldDate)
        {
            return _repository.FindAppointment(doctor, patient, oldDate);
                }

        public Appointment FindByID(int appointmentID)
        {
            return _repository.FindByID(appointmentID);
        }

        public List<Examination> FindByPatientID(int patientId)
        {
            return _repository.FindByPatientID(patientId);
        }

        public List<Examination> GetExaminations()
        {
            return _repository.GetExaminations();
        }

        public int GetNewID()
        {
            return _repository.GetNewID();
        }

        public void Remove(Examination examination)
        {
            _repository.Remove(examination);
        }

        public bool Update(Examination examination)
        {
            return _repository.Update(examination);
        }
    }
}
