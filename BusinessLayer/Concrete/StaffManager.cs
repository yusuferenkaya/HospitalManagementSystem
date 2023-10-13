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
    public class StaffManager : IStaffService
    {
        IStaffDal _staffDal;

        public StaffManager(IStaffDal staffDal)
        {
            _staffDal = staffDal;
        }

        public Staff GetByID(int id)
        {
            return _staffDal.Get(x => x.staffID == id);
        }

        public List<Staff> GetList()
        {
            return _staffDal.List();
        }

        public void StaffAdd(Staff staff)
        {
            _staffDal.Insert(staff);

        }

        public void StaffDelete(Staff staff)
        {
            _staffDal.Delete(staff);
        }

        public void StaffUpdate(Staff staff)
        {
            _staffDal.Update(staff);
        }
    }
}
