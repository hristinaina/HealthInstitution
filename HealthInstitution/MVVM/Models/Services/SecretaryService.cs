using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.Models.Services
{
    public static class SecretaryService
    {

        public static void ApproveChange(ExaminationChange request)
        {
            request.Resolved = true;
            //created
            if (request.ChangeStatus == 0)
            {
                // Milicina funkcija za kreiranje novih pregleda --- bitne sve provjere ---- ako vrati false izbaciti dijalog
            }
            //edited
            else if (request.ChangeStatus.ToString() == "EDITED")
            {
                // Milicina funkcija za editovanje pregleda --- bitne sve provjere ---- ako vrati false izbaciti dijalog
            }
            //deleted
            else if (request.ChangeStatus.ToString() == "DELETED")
            {
                Institution.Instance().ExaminationRepository.Delete(request.AppointmentID);
            }

        }

        public static void RejectChange(ExaminationChange request)
        {
            request.Resolved = true;
            string message = "This request has been rejected.";
            MessageBox.Show(message);

            if (request.ChangeStatus == 0)
            {
                // do nothing
            }
            //edited
            else if (request.ChangeStatus.ToString() == "EDITED")
            {
                
            }
            //deleted
            else if (request.ChangeStatus.ToString() == "DELETED")
            {
                // do nothing
            }
        }

        public static void RemoveOutdatedRequests()
        {
            foreach (ExaminationChange request in Institution.Instance().ExaminationChangeRepository.Changes)
            {
                if (!request.Resolved && request.NewDate <= DateTime.Now)
                {
                    request.ChangeStatus = Models.Enumerations.AppointmentStatus.DELETED;
                    request.Resolved = true;

                    // ?to delete appointment as well or not? 
                    Institution.Instance().ExaminationRepository.Delete(request.AppointmentID);
                }
            }
        }
    }
}
