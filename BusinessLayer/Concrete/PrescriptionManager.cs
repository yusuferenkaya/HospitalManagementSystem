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
    public class PrescriptionManager : IPrescriptionService
    {
        IPrescriptionDal _prescriptionDal;

        public PrescriptionManager(IPrescriptionDal prescriptionDal)
        {
            _prescriptionDal = prescriptionDal;
        }

        public Prescription GetByID(int id)
        {
            return _prescriptionDal.Get(x => x.prescriptionID == id);
        }

        public List<Prescription> GetList()
        {
            return _prescriptionDal.List();
        }

        public void PrescriptionAdd(Prescription prescription)
        {
            _prescriptionDal.Insert(prescription);

        }

        public void PrescriptionDelete(Prescription prescription)
        {
            _prescriptionDal.Delete(prescription);
        }

        public void PrescriptionUpdate(Prescription prescription)
        {
            _prescriptionDal.Update(prescription);
        }
    }
}
