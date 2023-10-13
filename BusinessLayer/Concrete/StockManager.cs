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
    public class StockManager : IStockService
    {

        IStockDal _stockDal;

        public StockManager(IStockDal stockDal)
        {
            _stockDal = stockDal;
        }

        public Stock GetByID(int id)
        {
            return _stockDal.Get(x => x.stockID == id);
        }

        public List<Stock> GetList()
        {
            return _stockDal.List();
        }

        public void StockAdd(Stock stock)
        {
            _stockDal.Insert(stock);
         
        }

        public void StockDelete(Stock stock)
        {
            _stockDal.Delete(stock);
        }

        public void StockUpdate(Stock stock)
        {
            _stockDal.Update(stock);
        }
    }
}

