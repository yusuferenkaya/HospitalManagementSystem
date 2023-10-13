using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Abstract
{
    public interface IPrescriptionService
    {
        List<Prescription> GetList();

        void PrescriptionAdd(Prescription prescription);

        Prescription GetByID(int id);

        void PrescriptionDelete(Prescription prescription);

        void PrescriptionUpdate(Prescription prescription);
    }
}
