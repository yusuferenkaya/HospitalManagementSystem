using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class PatientManager : IPatientService
    {
        IPatientDal _patientDal;

        public PatientManager(IPatientDal patientDal)
        {
            _patientDal = patientDal;
        }

        public Patient GetByEmail(string patientEmail)
        {
            return _patientDal.Get(x => x.patientEmail == patientEmail);
        }

        public Patient GetByID(int id)
        {
            return _patientDal.Get(x => x.patientID == id);
        }

        public Patient GetByUserID(int id)
        {
            return _patientDal.Get(x => x.userID == id);
        }

        public List<Patient> GetList()
        {
            return _patientDal.List();
        }

        public void PatientAdd(Patient patient)
        {
            _patientDal.Insert(patient);

        }

        public void PatientDelete(Patient patient)
        {
            _patientDal.Delete(patient);
        }

        public void PatientUpdate(Patient patient)
        {
            _patientDal.Update(patient);
        }
    }
}
