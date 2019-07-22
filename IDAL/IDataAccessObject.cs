using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace cms_webservice.IDAL
{
    public interface IDataAccessObject
    {
        IDatabase Db { get;set;}
        string Table { get;}
        string SelectCommand { get;}
        bool CanUpdate { get;}
        DbDataAdapter Adapter { get;}

        System.Data.DataTable Select(string args);
        object SelectScalar(string arg);
        void Fill(DataTable table, string args);
        int Insert(DataTable table);
        int Update(string sql);

        int Delete(string args);

        DbTransaction Transaction { get;set;}


        int ExecuteNonQuery(string sql);


        object MakeUid();

    }
}
