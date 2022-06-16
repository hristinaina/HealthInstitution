namespace HealthInstitution.Core
{
    public class AppointmentRefference
    {
        private readonly int _appoinmentId;
        private readonly int _doctorId;
        private readonly int _patientId;
        private readonly int _roomId;
        private readonly int _perscriptionId;

        public AppointmentRefference(int appointmentId, int doctoriId, int patientId,
                                     int roomId, int perscriptionId)
        {
            _appoinmentId = appointmentId;
            _doctorId = doctoriId;
            _patientId = patientId;
            _roomId = roomId;
            _perscriptionId = perscriptionId;
        }
    }
}
