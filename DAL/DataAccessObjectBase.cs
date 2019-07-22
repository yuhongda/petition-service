using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using cms_webservice.IDAL;

namespace cms_webservice.DAL
{
    public class DataAccessObjectBase : IDataAccessObject
    {
        private const string STR_SELECT = "SELECT * FROM {0}";
        //private const string STR_DELETE = "DELETE FROM {0} {1}";
        //private const string STR_表名必添 = "表名必添";
        //private const string STR_没有初始化的SQL命令 = "没有初始化的SQL命令";

        string m_sql = String.Empty;
        string m_table = String.Empty;
        bool m_canUpdate = false;//是否可以驱动Update
        bool m_canUseArg = false;//是否支持Select方法的参数

        IDatabase m_Db;
        DbCommand m_selectCommand;
        DbDataAdapter m_adapter;
        DbTransaction m_transaction;


        #region 构造
        public DataAccessObjectBase(string table)
        {
            this.Db = new MicrosoftDatabase();

            DbConnection conn = Db.CreateConnection();
            try
            {
                m_selectCommand = Db.GetSqlStringCommand(string.Format(STR_SELECT,table));
                m_selectCommand.Connection = conn;
                m_adapter = Db.GetDataAdapter();
                m_adapter.SelectCommand = m_selectCommand;
                DbCommandBuilder builder = m_Db.GetCommandBuilder(m_adapter);
                m_adapter.InsertCommand = builder.GetInsertCommand();
                m_adapter.DeleteCommand = builder.GetDeleteCommand();
                m_adapter.UpdateCommand = builder.GetUpdateCommand();
           
                this.m_canUpdate = true;
            }
            catch (System.Exception ex)
            {
                this.m_canUpdate = false;
            }
            conn.Close();
        }

        public DataAccessObjectBase()
        {
            this.Db = new MicrosoftDatabase();
        }

        #endregion

        #region IDataAccessObject 成员

        public IDatabase Db
        {
            get
            {
                return m_Db;
            }
            set
            {
                m_Db = value;
            }
        }

        public string Table
        {
            get
            {
                return m_table;
            }
        }

        public string SelectCommand
        {
            get { return m_sql; }
        }



        public virtual int Insert(DataTable table)
        {
            if (table.DataSet == null)
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.Tables.Add(table);
            }
            //table.TableName = m_table;


            if (this.m_canUpdate)
            {
                if (table.GetChanges() != null)
                {
                    if (this.m_transaction == null)
                    {
                        return m_Db.UpdateDataSet(table.DataSet, table.TableName, m_adapter.InsertCommand, m_adapter.UpdateCommand, m_adapter.DeleteCommand);
                    }
                    else
                    {
                        return m_Db.UpdateDataSet(table.DataSet, table.TableName, m_adapter.InsertCommand, m_adapter.UpdateCommand, m_adapter.DeleteCommand, m_transaction);
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }

        public virtual int Update(string sql)
        {
            return this.ExecuteNonQuery(sql);
        }

        public virtual int Update(string sql, params SqlParameter[] cmdParms)
        {
            DbCommand dbCommand = m_Db.GetSqlStringCommand(sql);
            foreach (SqlParameter sp in cmdParms)
            {
                m_Db.AddInParameter(dbCommand, sp.ParameterName, sp.DbType, sp.Value);
            }
            return m_Db.ExecuteNonQuery(dbCommand);
        }

        public virtual object SelectScalar(string sql)
        {
            object result = m_Db.ExecuteScalar(CommandType.Text, sql);
            if (result ==null)
            {
                result = string.Empty;
            }
            return result;
        }

        public virtual object SelectScalar(string sql, params SqlParameter[] cmdParms)
        {
            DbCommand dbCommand = m_Db.GetSqlStringCommand(sql);
            foreach (SqlParameter sp in cmdParms)
            {
                m_Db.AddInParameter(dbCommand, sp.ParameterName, sp.DbType, sp.Value);
            }
            object result = m_Db.ExecuteScalar(dbCommand);
            if (result == null)
            {
                result = string.Empty;
            }
            return result;
        }

        public virtual int Delete(string sql)
        {
          return this.ExecuteNonQuery(sql);
        }

        public virtual int Delete(string sql, params SqlParameter[] cmdParms)
        {
            DbCommand dbCommand = m_Db.GetSqlStringCommand(sql);
            foreach (SqlParameter sp in cmdParms)
            {
                m_Db.AddInParameter(dbCommand, sp.ParameterName, sp.DbType, sp.Value);
            }
            return m_Db.ExecuteNonQuery(dbCommand);
        }

        public virtual int ExecuteStoredProcedureNonQuery(string strStoredProcedureName, params object[] paramsValues)
        {
            return m_Db.ExecuteNonQuery(strStoredProcedureName, paramsValues);
        }

        /// <summary>
        /// 检测一个记录是否存在(SQL语句方式)
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public object CheckExists(string strSql, params SqlParameter[] cmdParms)
        {
            DbCommand dbCommand = m_Db.GetSqlStringCommand(strSql);
            //BuildDBParameter(db, dbCommand, cmdParms);
            BuildDBParameter(dbCommand, cmdParms);
            object obj = m_Db.ExecuteScalar(dbCommand);
            //int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                obj = null;
            }
            else
            {

            }

            return obj;
        }

        /// <summary>
        /// 检测一个记录是否存在(SQL语句方式)
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            DbCommand dbCommand = m_Db.GetSqlStringCommand(strSql);
            //BuildDBParameter(db, dbCommand, cmdParms);
            BuildDBParameter(dbCommand, cmdParms);
            object obj = m_Db.ExecuteScalar(dbCommand);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        /// <summary>
        /// 执行Insert/Delete/Update语句，返回是否成功执行

        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>成功执行返回true，否则返回false</returns>
        public bool ExecuteSql(string strSql)
        {
            try
            {
                DbCommand dbCommand = m_Db.GetSqlStringCommand(strSql);
                int row = m_Db.ExecuteNonQuery(dbCommand);
                if (row > 0) { return true; }
                else { return false; }

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 执行Insert/Delete/Update语句，返回是否成功执行(带条件的情况下)
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>成功执行返回true，否则返回false</returns>
        public bool ExecuteSql(string strSql, params SqlParameter[] cmdParms)
        {
            try
            {
                DbCommand dbCommand = m_Db.GetSqlStringCommand(strSql);
                //BuildDBParameter(db, dbCommand, cmdParms);
                BuildDBParameter(dbCommand, cmdParms);
                int row = m_Db.ExecuteNonQuery(dbCommand);
                if (row > 0) { return true; }
                else { return false; }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>DataSet</returns>
        public DataTable Select(string strSql)
        {
            DbCommand dbCommand = m_Db.GetSqlStringCommand(strSql);
            return m_Db.ExecuteDataSet(dbCommand).Tables[0];
        }

        /// <summary>
        /// 执行查询语句，返回DataTable(带条件的情况下)
        /// </summary>
        /// <param name="strSql">查询语句</param>
        /// <returns>DataTable</returns>
        public DataTable Select(string strSql, params SqlParameter[] cmdParms)
        {
            try
            {
                DbCommand dbCommand = m_Db.GetSqlStringCommand(strSql);
                BuildDBParameter(dbCommand, cmdParms);
                return m_Db.ExecuteDataSet(dbCommand).Tables[0];

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。

        /// </summary>
        /// <param name="strSql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string strSql, params SqlParameter[] cmdParms)
        {
            DbCommand dbCommand = m_Db.GetSqlStringCommand(strSql);
            //BuildDBParameter(db, dbCommand, cmdParms);
            if (cmdParms != null)
                BuildDBParameter(dbCommand, cmdParms);
            object obj = m_Db.ExecuteScalar(dbCommand);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return null;
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        /// 执行多条增，删，改SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public bool ExecuteSqlTran(Dictionary<SqlParameter[], string> SQLStringList)
        {
            bool flag = false;
            using (DbConnection dbconn = m_Db.CreateConnection())
            {
                dbconn.Open();
                DbTransaction dbtran = dbconn.BeginTransaction();
                try
                {
                    foreach (KeyValuePair<SqlParameter[], string> myDE in SQLStringList)
                    {
                        string strsql = myDE.Value;
                        SqlParameter[] cmdParms = myDE.Key;
                        if (strsql.Trim().Length > 1)
                        {
                            DbCommand dbCommand = m_Db.GetSqlStringCommand(strsql);
                            BuildDBParameter(dbCommand, cmdParms);
                            m_Db.ExecuteNonQuery(dbCommand, dbtran);
                        }
                    }
                    dbtran.Commit();
                    flag = true;
                }
                catch(Exception ex)
                {
                    dbtran.Rollback();

                }
                finally
                {
                    dbconn.Close();
                }
            }
            return flag;
        }

        /// <summary>
        /// 加载参数
        /// </summary>
        private void BuildDBParameter(DbCommand dbCommand, params SqlParameter[] cmdParms)
        {
            if (cmdParms != null)
            {
                foreach (SqlParameter sp in cmdParms)
                {
                    m_Db.AddInParameter(dbCommand, sp.ParameterName, sp.DbType, sp.Value);
                }
            }
        }

        /// <summary>
        /// 制造一组主键
        /// </summary>
        /// <returns></returns>
        public virtual object MakeUid()
        {
            return Guid.NewGuid().ToString();
        }

        public DbDataAdapter Adapter
        {
            get
            {
                return this.m_adapter;
            }
        }

        /// <summary>
        /// 是否可更新
        /// </summary>
        public bool CanUpdate
        {
            get { return m_canUpdate; }
            set { m_canUpdate = value; }
        }


        public int ExecuteNonQuery(string sql)
        {
            if (this.m_transaction == null)
            {
                return m_Db.ExecuteNonQuery(CommandType.Text, sql);
            }
            else
            {
                return m_Db.ExecuteNonQuery(m_transaction, CommandType.Text, sql);
            }
        }


        public DbTransaction Transaction
        {
            get
            {
                return m_transaction;
            }
            set
            {
                m_transaction = value;
            }
        }



        public void Fill(DataTable table, string args)
        {

            string _old = m_adapter.SelectCommand.CommandText;
            if (args != null)
            {
                m_adapter.SelectCommand.CommandText = _old + args;
            }
            this.m_adapter.Fill(table);
            m_adapter.SelectCommand.CommandText = _old;

        }

        #endregion
    }
}
