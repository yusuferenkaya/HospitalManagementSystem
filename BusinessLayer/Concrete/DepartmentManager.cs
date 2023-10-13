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
    public class DepartmentManager: IDepartmentService
    {
        IDepartmentDal _departmentDal;

        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        public Department GetByID(int id)
        {
            return _departmentDal.Get(x => x.departmentID == id);
        }

        public List<Department> GetList()
        {
            return _departmentDal.List();
        }

        public void DepartmentAdd(Department department)
        {
            _departmentDal.Insert(department);

        }

        public void DepartmentDelete(Department department)
        {
            _departmentDal.Delete(department);
        }

        public void DepartmentUpdate(Department department)
        {
            _departmentDal.Update(department);
        }
    }
}
