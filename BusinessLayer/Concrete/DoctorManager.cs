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
    public class DoctorManager : IDoctorService
    {
        IDoctorDal _doctorDal;

        public DoctorManager(IDoctorDal doctorDal)
        {
            _doctorDal = doctorDal;
        }

        public Doctor GetByID(int id)
        {
            return _doctorDal.Get(x => x.doctorID == id);
        }

        public List<Doctor> GetList()
        {
            return _doctorDal.List();
        }

        public void DoctorAdd(Doctor doctor)
        {
            _doctorDal.Insert(doctor);

        }

        public void DoctorDelete(Doctor doctor)
        {
            _doctorDal.Delete(doctor);
        }

        public void DoctorUpdate(Doctor doctor)
        {
            _doctorDal.Update(doctor);
        }

        public List<Doctor> GetListByID(int id)
        {
            return _doctorDal.List(x => x.departmentID == id);
        }

        public Doctor GetByEmail(string doctorEmail)
        {
            return _doctorDal.Get(x => x.doctorEmail == doctorEmail);
        }
    }
}
