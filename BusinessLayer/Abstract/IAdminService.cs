using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        List<Admin> GetList();

        void AdminAdd(Admin admin);

        Admin GetByID(int id);

        Admin GetByEmail(string adminEmail);
        void AdminDelete(Admin admin);

        void AdminUpdate(Admin admin);

        List<Admin> GetListByID(int id);
    }
}
