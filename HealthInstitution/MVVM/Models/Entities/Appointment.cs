namespace HealthInstitution.MVVM.Models.Entities
{
    public class Appointment
    {
        private int _id;
        private Doctor _doctor;
        private Patient _patient;
        private Room _room;
        private string _date;
        private bool _isEmergency;
        private bool _isDone;
        private string _anamnesis;
        private Perscription _perscription;
        private AppointmentReview _review;

        public Appointment(int id, string date, bool isEmergency, AppointmentReview review)
        {
            _id = id;
            _doctor = null;
            _patient = null;
            _room = null;
            _date = date;
            _isEmergency = isEmergency;
            _isDone = false;
            _anamnesis = "";
            _perscription = null;
            _review = review;
        }

        public int GetId() => _id;
        public void SetId(int id) => _id = id;
        public Doctor GetDoctor() => _doctor;
        public void SetDoctor(Doctor doctor) => _doctor = doctor;
        public Patient GetPatient() => _patient;
        public void SetPatient(Patient patient) => _patient = patient;
        public Room GetRoom() => _room;
        public void SetRoom(Room room) => _room = room;
        public string GetDate() => _date;
        public void SetDate(string date) => _date = date;
        public bool GetEmergency() => _isEmergency;
        public void SetEmergency(bool emergency) => _isEmergency = emergency;
        public bool GetDone() => _isDone;
        public void SetDone(bool done) => _isDone = done;
        public string GetAnamnesis() => _anamnesis;
        public void SetAnamnesis() => _anamnesis = _anamnesis;
        public Perscription GetPerscription() => _perscription;
        public void SetPerscription(Perscription perscription) => _perscription = perscription;
        public AppointmentReview GetReview() => _review;
        public void SetReview(AppointmentReview review) => _review = review;
    }
}