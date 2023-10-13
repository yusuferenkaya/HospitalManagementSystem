using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Abstract
{
    public interface ILogService
    {
        List<Log> GetList();

        void LogAdd(Log log);

        Log GetByID(int id);

        void LogDelete(Log log);

        void LogUpdate(Log log);

        List<Log> GetListByID(int id);
    }
}
