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
    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public Admin GetByID(int id)
        {
            return _adminDal.Get(x => x.adminID == id);
        }

        public List<Admin> GetList()
        {
            return _adminDal.List();
        }

        public void AdminAdd(Admin admin)
        {
            _adminDal.Insert(admin);

        }

        public void AdminDelete(Admin admin)
        {
            _adminDal.Delete(admin);
        }

        public void AdminUpdate(Admin admin)
        {
            _adminDal.Update(admin);
        }

        public List<Admin> GetListByID(int id)
        {
            return _adminDal.List(x => x.adminID == id);
        }

        public Admin GetByEmail(string adminEmail)
        {
            return _adminDal.Get(x => x.adminEmail == adminEmail);
        }
    }
}
