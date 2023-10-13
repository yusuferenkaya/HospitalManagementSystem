using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Abstract
{
    public interface IDoctorService
    {
        List<Doctor> GetList();

        List<Doctor> GetListByID(int id);
        void DoctorAdd(Doctor doctor);

        Doctor GetByID(int id);

        Doctor GetByEmail(string doctorEmail);

        void DoctorDelete(Doctor doctor);

        void DoctorUpdate(Doctor doctor);


    }
}
