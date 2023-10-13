using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IDepartmentService
    {
        List<Department> GetList();

        void DepartmentAdd(Department department);

        Department GetByID(int id);

        void DepartmentDelete(Department department);

        void DepartmentUpdate(Department department);
    }
}
