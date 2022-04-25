namespace HealthInstitution.MVVM.Models.Entities
{
    public class Appointment
    {
        public int Id { set; get; }
        public Doctor Doctor{ set; get; }
        public Patient Patient{ set; get; }
        public Room Room { set; get; }
        public string Time { set; get; }
        public string Date { set; get; }
        public bool Emergency { set; get; }
        public bool Done { set; get; }
        public string Anamnesis { set; get; }
        public Perscription Perscription { set; get; }
    }
}