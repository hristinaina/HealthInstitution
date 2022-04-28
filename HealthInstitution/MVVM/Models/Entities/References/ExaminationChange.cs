
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
        private int _appointmentID;
        private AppointmentStatus _changeStatus;
        private DateTime _changeDate;
        private bool _resolved;

        public int PatientID { get => this._patientId; set => this._patientId = value; }
        public int AppointmentID { get => this._appointmentID; set => this._appointmentID = value; }
        public AppointmentStatus ChangeStatus { get => this._changeStatus; set => this._changeStatus = value; }
        public DateTime ChangeDate { get => this._changeDate; set => this._changeDate = value; }
        public bool Resolved { get => this._resolved; set => this._resolved = value; }

        public ExaminationChange()
        {
        }

    }
}
