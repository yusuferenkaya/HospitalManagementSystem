using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Abstract
{
    public interface IAnnualLeaveService
    {
        List<AnnualLeave> GetList();

        void AnnualLeaveAdd(AnnualLeave annualLeave);

        AnnualLeave GetByID(int id);

        void AnnualLeaveDelete(AnnualLeave annualLeave);

        void AnnualLeaveUpdate(AnnualLeave annualLeave);

        List<AnnualLeave> GetListByID(int id);
    }
}
