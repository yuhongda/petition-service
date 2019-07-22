using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using cms_webservice.IDAL;

namespace cms_webservice.DAL
{
    public class MicrosoftDatabase : IDatabase
    {
        Database db;

        public MicrosoftDatabase()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        public MicrosoftDatabase(string configName)
        {
            db = DatabaseFactory.CreateDatabase(configName);
        }

        public Database DdBase()
        {
            return db;
        }

        #region IDatabase ≥…‘±

        public string ConnectionStringWithoutCredentials
        {
            get { return db.ConnectionStringWithoutCredentials; }
        }

        public void AddInParameter(System.Data.Common.DbCommand command, string name, System.Data.DbType dbType)
        {
            db.AddInParameter(command, name, dbType);
        }

        public void AddInParameter(System.Data.Common.DbCommand command, string name, System.Data.DbType dbType, object value)
        {
            db.AddInParameter(command, name, dbType, value);
        }

        public void AddInParameter(System.Data.Common.DbCommand command, string name, System.Data.DbType dbType, string sourceColumn, System.Data.DataRowVersion sourceVersion)
        {
            db.AddInParameter(command, name, dbType, sourceColumn, sourceVersion);
        }

        public void AddOutParameter(System.Data.Common.DbCommand command, string name, System.Data.DbType dbType, int size)
        {
            db.AddOutParameter(command, name, dbType, size);
        }

        public void AddParameter(System.Data.Common.DbCommand command, string name, System.Data.DbType dbType, System.Data.ParameterDirection direction, string sourceColumn, System.Data.DataRowVersion sourceVersion, object value)
        {
            db.AddParameter(command, name, dbType, direction, sourceColumn, sourceVersion, value);
        }

        public void AddParameter(System.Data.Common.DbCommand command, string name, System.Data.DbType dbType, int size, System.Data.ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, System.Data.DataRowVersion sourceVersion, object value)
        {
            db.AddParameter(command, name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
        }

        public string BuildParameterName(string name)
        {
            return db.BuildParameterName(name);
        }

        public System.Data.Common.DbConnection CreateConnection()
        {
            return db.CreateConnection();
        }

        public void DiscoverParameters(System.Data.Common.DbCommand command)
        {
            db.DiscoverParameters(command);
        }

        public System.Data.DataSet ExecuteDataSet(System.Data.Common.DbCommand command)
        {
            return db.ExecuteDataSet(command);
        }

        public System.Data.DataSet ExecuteDataSet(System.Data.CommandType commandType, string commandText)
        {
            return db.ExecuteDataSet(commandType, commandText);
        }

        public System.Data.DataSet ExecuteDataSet(System.Data.Common.DbCommand command, System.Data.Common.DbTransaction transaction)
        {
            return db.ExecuteDataSet(command, transaction);
        }

        public System.Data.DataSet ExecuteDataSet(string storedProcedureName, params object[] parameterValues)
        {
            return db.ExecuteDataSet(storedProcedureName, parameterValues);
        }

        public System.Data.DataSet ExecuteDataSet(System.Data.Common.DbTransaction transaction, System.Data.CommandType commandType, string commandText)
        {
            return db.ExecuteDataSet(transaction, commandType, commandText);
        }

        public System.Data.DataSet ExecuteDataSet(System.Data.Common.DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            return db.ExecuteDataSet(transaction, storedProcedureName, parameterValues);
        }

        public int ExecuteNonQuery(System.Data.Common.DbCommand command)
        {
            return db.ExecuteNonQuery(command);
        }

        public int ExecuteNonQuery(System.Data.CommandType commandType, string commandText)
        {
            return db.ExecuteNonQuery(commandType, commandText);
        }

        public int ExecuteNonQuery(System.Data.Common.DbCommand command, System.Data.Common.DbTransaction transaction)
        {
            return db.ExecuteNonQuery(command, transaction);
        }

        public int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues)
        {
            return db.ExecuteNonQuery(storedProcedureName, parameterValues);
        }

        public int ExecuteNonQuery(System.Data.Common.DbTransaction transaction, System.Data.CommandType commandType, string commandText)
        {
            return db.ExecuteNonQuery(transaction, commandType, commandText);
        }

        public int ExecuteNonQuery(System.Data.Common.DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            return db.ExecuteNonQuery(transaction, storedProcedureName, parameterValues);
        }

        public System.Data.IDataReader ExecuteReader(System.Data.Common.DbCommand command)
        {
            return db.ExecuteReader(command);
        }

        public System.Data.IDataReader ExecuteReader(System.Data.CommandType commandType, string commandText)
        {
            return db.ExecuteReader(commandType, commandText);
        }

        public System.Data.IDataReader ExecuteReader(System.Data.Common.DbCommand command, System.Data.Common.DbTransaction transaction)
        {
            return db.ExecuteReader(command, transaction);
        }

        public System.Data.IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
        {
            return db.ExecuteReader(storedProcedureName, parameterValues);
        }

        public System.Data.IDataReader ExecuteReader(System.Data.Common.DbTransaction transaction, System.Data.CommandType commandType, string commandText)
        {
            return db.ExecuteReader(transaction, commandType, commandText);
        }

        public System.Data.IDataReader ExecuteReader(System.Data.Common.DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            return db.ExecuteReader(transaction, storedProcedureName, parameterValues);
        }

        public object ExecuteScalar(System.Data.Common.DbCommand command)
        {
            return db.ExecuteScalar(command);
        }

        public object ExecuteScalar(System.Data.CommandType commandType, string commandText)
        {
            return db.ExecuteScalar(commandType, commandText);
        }

        public object ExecuteScalar(System.Data.Common.DbCommand command, System.Data.Common.DbTransaction transaction)
        {
            return db.ExecuteScalar(command, transaction);
        }

        public object ExecuteScalar(string storedProcedureName, params object[] parameterValues)
        {
            return db.ExecuteScalar(storedProcedureName, parameterValues);
        }

        public object ExecuteScalar(System.Data.Common.DbTransaction transaction, System.Data.CommandType commandType, string commandText)
        {
            return db.ExecuteScalar(transaction, commandType, commandText);
        }

        public object ExecuteScalar(System.Data.Common.DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            return db.ExecuteScalar(transaction, storedProcedureName, parameterValues);
        }

        public System.Data.Common.DbDataAdapter GetDataAdapter()
        {
            return db.GetDataAdapter();
        }

        public object GetInstrumentationEventProvider()
        {
            return db.GetInstrumentationEventProvider();
        }

        public object GetParameterValue(System.Data.Common.DbCommand command, string name)
        {
            return db.GetParameterValue(command, name);
        }

        public System.Data.Common.DbCommand GetSqlStringCommand(string query)
        {
            return db.GetSqlStringCommand(query);
        }

        public System.Data.Common.DbCommand GetStoredProcCommand(string storedProcedureName)
        {
            return db.GetStoredProcCommand(storedProcedureName);
        }

        public System.Data.Common.DbCommand GetStoredProcCommand(string storedProcedureName, params object[] parameterValues)
        {
            return db.GetStoredProcCommand(storedProcedureName, parameterValues);
        }

        public System.Data.Common.DbCommand GetStoredProcCommandWithSourceColumns(string storedProcedureName, params string[] sourceColumns)
        {
            return db.GetStoredProcCommandWithSourceColumns(storedProcedureName, sourceColumns);
        }

        public void LoadDataSet(System.Data.Common.DbCommand command, System.Data.DataSet dataSet, string tableName)
        {
            db.LoadDataSet(command, dataSet, tableName);
        }

        public void LoadDataSet(System.Data.Common.DbCommand command, System.Data.DataSet dataSet, string[] tableNames)
        {
            db.LoadDataSet(command, dataSet, tableNames);
        }

        public void LoadDataSet(System.Data.CommandType commandType, string commandText, System.Data.DataSet dataSet, string[] tableNames)
        {
            db.LoadDataSet(commandType, commandText, dataSet, tableNames);
        }

        public void LoadDataSet(System.Data.Common.DbCommand command, System.Data.DataSet dataSet, string tableName, System.Data.Common.DbTransaction transaction)
        {
            db.LoadDataSet(command, dataSet, tableName, transaction);
        }

        public void LoadDataSet(System.Data.Common.DbCommand command, System.Data.DataSet dataSet, string[] tableNames, System.Data.Common.DbTransaction transaction)
        {
            db.LoadDataSet(command, dataSet, tableNames, transaction);
        }

        public void LoadDataSet(string storedProcedureName, System.Data.DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            db.LoadDataSet(storedProcedureName, dataSet, tableNames, parameterValues);
        }

        public void LoadDataSet(System.Data.Common.DbTransaction transaction, System.Data.CommandType commandType, string commandText, System.Data.DataSet dataSet, string[] tableNames)
        {
            db.LoadDataSet(transaction, commandType, commandText, dataSet, tableNames);
        }

        public void LoadDataSet(System.Data.Common.DbTransaction transaction, string storedProcedureName, System.Data.DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            db.LoadDataSet(transaction, storedProcedureName, dataSet, tableNames, parameterValues);
        }

        public int UpdateDataSet(System.Data.DataSet dataSet, string tableName, System.Data.Common.DbCommand insertCommand, System.Data.Common.DbCommand updateCommand, System.Data.Common.DbCommand deleteCommand, System.Data.Common.DbTransaction transaction)
        {
            return db.UpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand, transaction);
        }


        public int UpdateDataSet(System.Data.DataSet dataSet, string tableName, System.Data.Common.DbCommand insertCommand, System.Data.Common.DbCommand updateCommand, System.Data.Common.DbCommand deleteCommand)
        {
            return db.UpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand, UpdateBehavior.Transactional);
        }

        public DbCommandBuilder GetCommandBuilder()
        {
            return GetCommandBuilder(null);
        }
        public DbCommandBuilder GetCommandBuilder(DbDataAdapter adapter)
        {
            string dbType = db.GetDataAdapter().GetType().ToString();
            Type createType = null;
            switch (dbType)
            {

                case "System.Data.Odbc.OdbcDataAdapter":
                    createType = new System.Data.Odbc.OdbcCommandBuilder().GetType();
                    break;
                case "System.Data.OleDb.OleDbDataAdapter":
                    createType = new System.Data.OleDb.OleDbCommandBuilder().GetType();
                    break;
                case "System.Data.SqlClient.SqlDataAdapter":
                    createType = new System.Data.SqlClient.SqlCommandBuilder().GetType();
                    break;
                /*case "System.Data.SqlServerCe.SqlCeCommand":
                    createType = "System.Data.SqlServerCe.SqlCeCommandBuilder";
                    break;*/
                default:
                    return null;
            }

            if (adapter != null)
            {
                return (DbCommandBuilder)System.Activator.CreateInstance(createType, adapter);
            }
            else
            {
                return (DbCommandBuilder)System.Activator.CreateInstance(createType);
            }


        }
        #endregion
    }
}
