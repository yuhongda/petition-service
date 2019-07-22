using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace cms_webservice.IDAL
{
    public interface IDatabase
    {
        string ConnectionStringWithoutCredentials { get; }
        void AddInParameter(DbCommand command, string name, DbType dbType);
        void AddInParameter(DbCommand command, string name, DbType dbType, object value);
        void AddInParameter(DbCommand command, string name, DbType dbType, string sourceColumn, DataRowVersion sourceVersion);
        void AddOutParameter(DbCommand command, string name, DbType dbType, int size);
        void AddParameter(DbCommand command, string name, DbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value);
        void AddParameter(DbCommand command, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value);
        string BuildParameterName(string name);
        DbConnection CreateConnection();
        void DiscoverParameters(DbCommand command);
        System.Data.DataSet ExecuteDataSet(DbCommand command);
        System.Data.DataSet ExecuteDataSet(CommandType commandType, string commandText);
        System.Data.DataSet ExecuteDataSet(DbCommand command, DbTransaction transaction);
        System.Data.DataSet ExecuteDataSet(string storedProcedureName, params object[] parameterValues);
        System.Data.DataSet ExecuteDataSet(DbTransaction transaction, CommandType commandType, string commandText);
        System.Data.DataSet ExecuteDataSet(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);
        int ExecuteNonQuery(DbCommand command);
        int ExecuteNonQuery(CommandType commandType, string commandText);
        int ExecuteNonQuery(DbCommand command, DbTransaction transaction);
        int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues);
        int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText);
        int ExecuteNonQuery(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);
        IDataReader ExecuteReader(DbCommand command);
        IDataReader ExecuteReader(CommandType commandType, string commandText);
        IDataReader ExecuteReader(DbCommand command, DbTransaction transaction);
        IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues);
        IDataReader ExecuteReader(DbTransaction transaction, CommandType commandType, string commandText);
        IDataReader ExecuteReader(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);
        object ExecuteScalar(DbCommand command);
        object ExecuteScalar(CommandType commandType, string commandText);
        object ExecuteScalar(DbCommand command, DbTransaction transaction);
        object ExecuteScalar(string storedProcedureName, params object[] parameterValues);
        object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText);
        object ExecuteScalar(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);
        DbDataAdapter GetDataAdapter();
        object GetInstrumentationEventProvider();
        object GetParameterValue(DbCommand command, string name);
        DbCommand GetSqlStringCommand(string query);
        DbCommand GetStoredProcCommand(string storedProcedureName);
        DbCommand GetStoredProcCommand(string storedProcedureName, params object[] parameterValues);
        DbCommand GetStoredProcCommandWithSourceColumns(string storedProcedureName, params string[] sourceColumns);
        void LoadDataSet(DbCommand command, System.Data.DataSet dataSet, string tableName);
        void LoadDataSet(DbCommand command, System.Data.DataSet dataSet, string[] tableNames);
        void LoadDataSet(CommandType commandType, string commandText, System.Data.DataSet dataSet, string[] tableNames);
        void LoadDataSet(DbCommand command, System.Data.DataSet dataSet, string tableName, DbTransaction transaction);
        void LoadDataSet(DbCommand command, System.Data.DataSet dataSet, string[] tableNames, DbTransaction transaction);
        void LoadDataSet(string storedProcedureName, System.Data.DataSet dataSet, string[] tableNames, params object[] parameterValues);
        void LoadDataSet(DbTransaction transaction, CommandType commandType, string commandText, System.Data.DataSet dataSet, string[] tableNames);
        void LoadDataSet(DbTransaction transaction, string storedProcedureName, System.Data.DataSet dataSet, string[] tableNames, params object[] parameterValues);
        int UpdateDataSet(System.Data.DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, DbTransaction transaction);
        int UpdateDataSet(System.Data.DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand);
        DbCommandBuilder GetCommandBuilder();
        DbCommandBuilder GetCommandBuilder(DbDataAdapter adapter);
    }
}
