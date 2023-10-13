using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Abstract
{
    public interface IPatientService
    {
        List<Patient> GetList();

        void PatientAdd(Patient patient);

        Patient GetByID(int id);

        void PatientDelete(Patient patient);

        void PatientUpdate(Patient patient);

        Patient GetByEmail(string patientEmail);
    }
}
