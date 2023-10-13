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
    public class LogManager : ILogService
    {
        ILogDal _logDal;

        public LogManager(ILogDal logDal)
        {
            _logDal = logDal;
        }

        public Log GetByID(int id)
        {
            return _logDal.Get(x => x.LogID == id);
        }

        public List<Log> GetList()
        {
            return _logDal.List();
        }

        public List<Log> GetListByID(int id)
        {
            throw new NotImplementedException();
        }

        public void LogAdd(Log log)
        {
            _logDal.Insert(log);

        }

        public void LogDelete(Log log)
        {
            _logDal.Delete(log);
        }

        public void LogUpdate(Log log)
        {
            _logDal.Update(log);
        }
    }
}
