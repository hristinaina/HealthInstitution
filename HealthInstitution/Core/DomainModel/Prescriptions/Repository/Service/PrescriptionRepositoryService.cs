using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class PrescriptionRepositoryService : IPrescriptionRepositoryService
    {

        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionRepositoryService()
        {
            _prescriptionRepository = Institution.Instance().PrescriptionRepository;
        }

        public void Add(Prescription prescription)
        {
            _prescriptionRepository.Add(prescription);
        }

        public Prescription FindByID(int id)
        {
            return _prescriptionRepository.FindByID(id);
        }

        public int GetNewID()
        {
            return _prescriptionRepository.GetNewID();
        }

        public List<Prescription> GetPrescriptions()
        {
            return _prescriptionRepository.GetPrescriptions();
        }
    }
}
