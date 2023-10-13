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
    public class AnnualLeaveManager : IAnnualLeaveService
    {
        IAnnualLeaveDal _annualLeaveDal;

        public AnnualLeaveManager(IAnnualLeaveDal annualLeaveDal)
        {
            _annualLeaveDal = annualLeaveDal;
        }

        public AnnualLeave GetByID(int id)
        {
            return _annualLeaveDal.Get(x => x.annualLeaveID == id);
        }

        public List<AnnualLeave> GetList()
        {
            return _annualLeaveDal.List();
        }

        public void AnnualLeaveAdd(AnnualLeave annualLeave)
        {
            _annualLeaveDal.Insert(annualLeave);

        }

        public void AnnualLeaveDelete(AnnualLeave annualLeave)
        {
            _annualLeaveDal.Delete(annualLeave);
        }

        public void AnnualLeaveUpdate(AnnualLeave annualLeave)
        {
            _annualLeaveDal.Update(annualLeave);
        }

        public List<AnnualLeave> GetListByID(int id)
        {
            return _annualLeaveDal.List(x => x.doctorID == id);
        }
    }
}
