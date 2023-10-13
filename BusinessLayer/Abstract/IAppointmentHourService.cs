using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Abstract
{
    public interface IAppointmentHourService
    {
        List<AppointmentHour> GetList();

        void AppointmentHourAdd(AppointmentHour appointmentHour);

        AppointmentHour GetByID(int id);

        void AppointmentHourDelete(AppointmentHour appointmentHour);

        void AppointmentHourUpdate(AppointmentHour appointmentHour);


    }
}
