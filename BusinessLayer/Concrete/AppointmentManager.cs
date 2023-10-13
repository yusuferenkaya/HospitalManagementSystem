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
    public class AppointmentManager : IAppointmentService
    {
        IAppointmentDal _appointmentDal;

        public AppointmentManager(IAppointmentDal appointmentDal)
        {
            _appointmentDal = appointmentDal;
        }

        public Appointment GetByID(int id)
        {
            return _appointmentDal.Get(x => x.appointmentID == id);
        }

        public List<Appointment> GetList()
        {
            return _appointmentDal.List();
        }

        public void AppointmentAdd(Appointment appointment)
        {
            _appointmentDal.Insert(appointment);

        }

        public void AppointmentDelete(Appointment appointment)
        {
            _appointmentDal.Delete(appointment);
        }

        public void AppointmentUpdate(Appointment appointment)
        {
            _appointmentDal.Update(appointment);
        }

        public List<Appointment> GetListByID(int id)
        {
            return _appointmentDal.List(x => x.doctorID == id);
        }

        public List<Appointment> GetListByIDForPatient(int id)
        {
            return _appointmentDal.List(x => x.patientID == id);
        }
    }
}
