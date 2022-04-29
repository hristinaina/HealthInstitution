
using HealthInstitution.MVVM.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities.References
{
    public class ExaminationChange
    {
        private int _patientId;
        private int _appointmentId;
        private AppointmentStatus _changeStatus;
        private DateTime _changeDate;
        private bool _resolved;
        private DateTime _newDate;

        public int PatientID { get => this._patientId; set => this._patientId = value; }
        public int AppointmentID { get => this._appointmentId; set => this._appointmentId = value; }
        public AppointmentStatus ChangeStatus { get => this._changeStatus; set => this._changeStatus = value; }
        public DateTime ChangeDate { get => this._changeDate; set => this._changeDate = value; }
        public bool Resolved { get => this._resolved; set => this._resolved = value; }
        public DateTime NewDate { get => this._newDate; set => this._newDate = value; }

        public ExaminationChange()
        {
        }

        public ExaminationChange(int patientID, int appointmentID, AppointmentStatus appointmentStatus, DateTime datetime, bool resolved)
        {
            _patientId = patientID;
            _appointmentId = appointmentID;
            _changeStatus = appointmentStatus;
            _changeDate = datetime;
            _resolved = resolved;
        }
    }
}
