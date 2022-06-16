namespace HealthInstitution.Core.Repository
{
    public interface IPrescriptionMedicineRepository
    {
        public PrescriptionMedicine FindByPrescriptionID(int prescriptionId);

        public void Add(PrescriptionMedicine prescriptionMedicine);
    }
}