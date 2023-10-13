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
    public class AppointmentHourManager : IAppointmentHourService
    {
        IAppointmentHourDal _appointmentHourDal;

        public AppointmentHourManager(IAppointmentHourDal appointmentHourDal)
        {
            _appointmentHourDal = appointmentHourDal;
        }

        public AppointmentHour GetByID(int id)
        {
            return _appointmentHourDal.Get(x => x.Id == id);
        }

        public List<AppointmentHour> GetList()
        {
            return _appointmentHourDal.List();
        }

        public void AppointmentHourAdd(AppointmentHour appointmentHour)
        {
            _appointmentHourDal.Insert(appointmentHour);

        }

        public void AppointmentHourDelete(AppointmentHour appointmentHour)
        {
            _appointmentHourDal.Delete(appointmentHour);
        }

        public void AppointmentHourUpdate(AppointmentHour appointmentHour)
        {
            _appointmentHourDal.Update(appointmentHour);
        }
    }
}
