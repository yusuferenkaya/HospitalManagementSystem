using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IStaffService
    {
        List<Staff> GetList();

        void StaffAdd(Staff staff);

        Staff GetByID(int id);

        void StaffDelete(Staff staff);

        void StaffUpdate(Staff staff);
    }
}
