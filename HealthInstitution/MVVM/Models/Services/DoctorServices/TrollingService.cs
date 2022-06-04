using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services.DoctorServices
{
    public class TrollingService
    {
        Patient _patient;
        private List<ExaminationChange> _changes;

        public TrollingService(Patient patient)
        {
            _patient = patient;
            _changes = patient.ExaminationChanges;
        }

        public bool IsTrolling()
        {
            if (GetEditingAttempts() > 5 || GetCreatingAttempts() > 8)
            {
                _patient.Block(BlockadeType.SYSTEM);
                return true;
            }
            return false;
        }

        private int GetCreatingAttempts()
        {
            int totalCreations = 0;
            foreach (ExaminationChange change in _changes)
            {
                if (change.ChangeStatus == AppointmentStatus.CREATED)
                {
                    totalCreations++;
                }
            }

            return totalCreations;
        }

        private int GetEditingAttempts()
        {
            int totalChanges = 0;
            foreach (ExaminationChange change in _changes)
            {
                if (change.ChangeStatus == AppointmentStatus.EDITED || change.ChangeStatus == AppointmentStatus.DELETED)
                {
                    totalChanges++;
                }
            }

            return totalChanges;
        }
    }
}
