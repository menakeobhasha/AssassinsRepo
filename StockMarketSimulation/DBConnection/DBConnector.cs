using Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection
{
    public class DBConnector : IDisposable
    {
        private string connectionString = string.Empty;
        private bool disposed;
        private bool isException;
        private SqlConnection oSqlConnection;
        private SqlTransaction oSqlTransaction;
        private OracleConnection oOracleConnection;
        private OracleTransaction oOracleTransaction;
        private List<Parameter> parameterCollection;
        private int _connectionType;

        public DBConnector(string connectionString, int connectionType)
        {
            this.connectionString = connectionString;
            this._connectionType = connectionType;
            this.OpenConnection();
            this.BeginTransaction();
            this.parameterCollection = new List<Parameter>();
            this.disposed = false;
        }

        ~DBConnector()
        {
            this.Dispose(false);
        }

        public string CommandText { get; set; }

        public bool IsException
        {
            get
            {
                return this.isException;
            }
            set
            {
                this.isException = value;
            }
        }

        public List<Parameter> Parameters
        {
            get
            {
                return this.parameterCollection;
            }
            set
            {
                this.parameterCollection = value;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;
            if (disposing)
            {
                if (this.isException)
                    this.RollbackTransaction();
                else
                    this.CommitTransaction();
                this.CloseConnection();
            }
            this.disposed = true;
        }

        private void BeginTransaction(IsolationLevel isolationLevel)
        {
            switch (this._connectionType)
            {
                case 1:
                    this.oSqlTransaction = this.oSqlConnection.BeginTransaction(isolationLevel);
                    break;
                case 2:
                    this.oOracleTransaction = this.oOracleConnection.BeginTransaction(isolationLevel);
                    break;
            }
        }

        private void BeginTransaction()
        {
            switch (this._connectionType)
            {
                case 1:
                    this.oSqlTransaction = this.oSqlConnection.BeginTransaction();
                    break;
                case 2:
                    this.oOracleTransaction = this.oOracleConnection.BeginTransaction();
                    break;
            }
        }

        private void CloseConnection()
        {
            switch (this._connectionType)
            {
                case 1:
                    if (this.oSqlConnection.State == ConnectionState.Closed)
                        break;
                    this.oSqlConnection.Close();
                    break;
                case 2:
                    if (this.oOracleConnection.State == ConnectionState.Closed)
                        break;
                    this.oOracleConnection.Close();
                    break;
            }
        }

        private void CommitTransaction()
        {
            switch (this._connectionType)
            {
                case 1:
                    this.oSqlTransaction.Commit();
                    break;
                case 2:
                    this.oOracleTransaction.Commit();
                    break;
            }
        }

        private void OpenConnection()
        {
            switch (this._connectionType)
            {
                case 1:
                    if (this.oSqlConnection == null)
                        this.oSqlConnection = new SqlConnection(this.connectionString);
                    this.oSqlConnection.Open();
                    break;
                case 2:
                    if (this.oOracleConnection == null)
                        this.oOracleConnection = new OracleConnection(this.connectionString);
                    this.oOracleConnection.Open();
                    break;
            }
        }

        private void RollbackTransaction()
        {
            switch (this._connectionType)
            {
                case 1:
                    if (this.oSqlTransaction == null)
                        break;
                    this.oSqlTransaction.Rollback();
                    break;
                case 2:
                    if (this.oOracleTransaction == null)
                        break;
                    this.oOracleTransaction.Rollback();
                    break;
            }
        }

        private List<SqlParameter> GetSqlParameters(List<Parameter> parameters)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            foreach (Parameter parameter in parameters)
            {
                SqlParameter sqlParameter = new SqlParameter(parameter.Name, parameter.Value);
                sqlParameterList.Add(sqlParameter);
            }
            return sqlParameterList;
        }

        private List<OracleParameter> GetOracleParameters(List<Parameter> parameters)
        {
            List<OracleParameter> oracleParameterList = new List<OracleParameter>();
            foreach (Parameter parameter in parameters)
            {
                OracleParameter oracleParameter = new OracleParameter(parameter.Name, parameter.Value);
                oracleParameterList.Add(oracleParameter);
            }
            return oracleParameterList;
        }

        public DataSet ExecuteDataSet()
        {
            DataSet dataSet = new DataSet();
            string str = this.CommandText;
            try
            {
                switch (this._connectionType)
                {
                    case 1:
                        str = str.Replace('?', '@');
                        break;
                    case 2:
                        str = str.Replace('?', ':');
                        break;
                }
                switch (this._connectionType)
                {
                    case 1:
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.CommandTimeout = Common.CommandTimeout;
                        sqlCommand.CommandText = str;
                        if (this.Parameters.Count > 0)
                            sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                        sqlCommand.Connection = this.oSqlConnection;
                        sqlCommand.Transaction = this.oSqlTransaction;
                        new SqlDataAdapter()
                        {
                            SelectCommand = sqlCommand
                        }.Fill(dataSet);
                        break;
                    case 2:
                        OracleCommand oracleCommand = new OracleCommand();
                        oracleCommand.CommandTimeout = Common.CommandTimeout;
                        oracleCommand.CommandText = str;
                        if (this.Parameters.Count > 0)
                            oracleCommand.Parameters.AddRange(this.GetOracleParameters(this.Parameters).ToArray());
                        oracleCommand.Connection = this.oOracleConnection;
                        oracleCommand.Transaction = this.oOracleTransaction;
                        new OracleDataAdapter()
                        {
                            SelectCommand = oracleCommand
                        }.Fill(dataSet);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
            return dataSet;
        }

        public DataSet ExecuteDataSet(string tableName)
        {
            DataSet dataSet = new DataSet();
            string str = this.CommandText;
            try
            {
                switch (this._connectionType)
                {
                    case 1:
                        str = str.Replace('?', '@');
                        break;
                    case 2:
                        str = str.Replace('?', ':');
                        break;
                }
                switch (this._connectionType)
                {
                    case 1:
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.CommandTimeout = Common.CommandTimeout;
                        sqlCommand.CommandText = str;
                        if (this.Parameters.Count > 0)
                            sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                        sqlCommand.Connection = this.oSqlConnection;
                        sqlCommand.Transaction = this.oSqlTransaction;
                        new SqlDataAdapter()
                        {
                            SelectCommand = sqlCommand
                        }.Fill(dataSet, tableName);
                        break;
                    case 2:
                        OracleCommand oracleCommand = new OracleCommand();
                        oracleCommand.CommandTimeout = Common.CommandTimeout;
                        oracleCommand.CommandText = str;
                        if (this.Parameters.Count > 0)
                            oracleCommand.Parameters.AddRange(this.GetOracleParameters(this.Parameters).ToArray());
                        oracleCommand.Connection = this.oOracleConnection;
                        oracleCommand.Transaction = this.oOracleTransaction;
                        new OracleDataAdapter()
                        {
                            SelectCommand = oracleCommand
                        }.Fill(dataSet, tableName);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
            return dataSet;
        }

        public IDataReader ExecuteProcedure()
        {
            string str = this.CommandText;
            IDataReader dataReader = (IDataReader)null;
            try
            {
                switch (this._connectionType)
                {
                    case 1:
                        str = str.Replace('?', '@');
                        break;
                    case 2:
                        str = str.Replace('?', ':');
                        break;
                }
                switch (this._connectionType)
                {
                    case 1:
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.CommandTimeout = Common.CommandTimeout;
                        sqlCommand.CommandText = str;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        if (this.Parameters.Count > 0)
                            sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                        sqlCommand.Connection = this.oSqlConnection;
                        sqlCommand.Transaction = this.oSqlTransaction;
                        dataReader = (IDataReader)sqlCommand.ExecuteReader();
                        break;
                    case 2:
                        OracleCommand oracleCommand = new OracleCommand();
                        oracleCommand.CommandTimeout = Common.CommandTimeout;
                        oracleCommand.CommandText = str;
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        if (this.Parameters.Count > 0)
                            oracleCommand.Parameters.AddRange(this.GetOracleParameters(this.Parameters).ToArray());
                        oracleCommand.Connection = this.oOracleConnection;
                        oracleCommand.Transaction = this.oOracleTransaction;
                        dataReader = (IDataReader)oracleCommand.ExecuteReader();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
            return dataReader;
        }

        public int ExecuteQuery()
        {
            int num = 0;
            string str = this.CommandText;
            try
            {
                switch (this._connectionType)
                {
                    case 1:
                        str = str.Replace('?', '@');
                        break;
                    case 2:
                        str = str.Replace('?', ':');
                        break;
                }
                switch (this._connectionType)
                {
                    case 1:
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.CommandTimeout = Common.CommandTimeout;
                        sqlCommand.CommandText = str;
                        sqlCommand.Connection = this.oSqlConnection;
                        sqlCommand.Transaction = this.oSqlTransaction;
                        if (this.Parameters.Count > 0)
                            sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                        num = sqlCommand.ExecuteNonQuery();
                        break;
                    case 2:
                        OracleCommand oracleCommand = new OracleCommand();
                        oracleCommand.CommandTimeout = Common.CommandTimeout;
                        oracleCommand.CommandText = str;
                        oracleCommand.Connection = this.oOracleConnection;
                        oracleCommand.Transaction = this.oOracleTransaction;
                        if (this.Parameters.Count > 0)
                            oracleCommand.Parameters.AddRange(this.GetOracleParameters(this.Parameters).ToArray());
                        num = oracleCommand.ExecuteNonQuery();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
            return num;
        }

        public IDataReader ExecuteReader()
        {
            string str = this.CommandText;
            IDataReader dataReader = (IDataReader)null;
            try
            {
                switch (this._connectionType)
                {
                    case 1:
                        str = str.Replace('?', '@');
                        break;
                    case 2:
                        str = str.Replace('?', ':');
                        break;
                }
                switch (this._connectionType)
                {
                    case 1:
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.CommandTimeout = Common.CommandTimeout;
                        sqlCommand.CommandText = str;
                        sqlCommand.Connection = this.oSqlConnection;
                        sqlCommand.Transaction = this.oSqlTransaction;
                        if (this.Parameters.Count > 0)
                            sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                        dataReader = (IDataReader)sqlCommand.ExecuteReader();
                        break;
                    case 2:
                        OracleCommand oracleCommand = new OracleCommand();
                        oracleCommand.CommandTimeout = Common.CommandTimeout;
                        oracleCommand.CommandText = str;
                        oracleCommand.Connection = this.oOracleConnection;
                        oracleCommand.Transaction = this.oOracleTransaction;
                        if (this.Parameters.Count > 0)
                            oracleCommand.Parameters.AddRange(this.GetOracleParameters(this.Parameters).ToArray());
                        dataReader = (IDataReader)oracleCommand.ExecuteReader();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
            return dataReader;
        }

        public object ExecuteScalar()
        {
            object obj = (object)null;
            string str = this.CommandText;
            try
            {
                switch (this._connectionType)
                {
                    case 1:
                        str = str.Replace('?', '@');
                        break;
                    case 2:
                        str = str.Replace('?', ':');
                        break;
                }
                switch (this._connectionType)
                {
                    case 1:
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.CommandTimeout = Common.CommandTimeout;
                        sqlCommand.CommandText = str;
                        if (this.Parameters.Count > 0)
                            sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                        sqlCommand.Connection = this.oSqlConnection;
                        sqlCommand.Transaction = this.oSqlTransaction;
                        obj = sqlCommand.ExecuteScalar();
                        break;
                    case 2:
                        OracleCommand oracleCommand = new OracleCommand();
                        oracleCommand.CommandTimeout = Common.CommandTimeout;
                        oracleCommand.CommandText = str;
                        if (this.Parameters.Count > 0)
                            oracleCommand.Parameters.AddRange(this.GetOracleParameters(this.Parameters).ToArray());
                        oracleCommand.Connection = this.oOracleConnection;
                        oracleCommand.Transaction = this.oOracleTransaction;
                        obj = oracleCommand.ExecuteScalar();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
            return obj;
        }

        public IDataReader ExecuteProcedureWithCursor()
        {
            string str = this.CommandText;
            IDataReader dataReader = (IDataReader)null;
            try
            {
                switch (this._connectionType)
                {
                    case 1:
                        str = str.Replace('?', '@');
                        break;
                    case 2:
                        str = str.Replace('?', ':');
                        break;
                }
                switch (this._connectionType)
                {
                    case 1:
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.CommandTimeout = 86400000;
                        sqlCommand.CommandText = str;
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        if (this.Parameters.Count > 0)
                            sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                        sqlCommand.Connection = this.oSqlConnection;
                        sqlCommand.Transaction = this.oSqlTransaction;
                        dataReader = (IDataReader)sqlCommand.ExecuteReader();
                        break;
                    case 2:
                        OracleCommand oracleCommand = new OracleCommand();
                        oracleCommand.CommandTimeout = Common.CommandTimeout;
                        oracleCommand.CommandText = str;
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        if (this.Parameters.Count > 0)
                            oracleCommand.Parameters.AddRange(this.GetOracleParameters(this.Parameters).ToArray());
                        oracleCommand.Parameters.Add("ora_cur", OracleType.Cursor).Direction = ParameterDirection.Output;
                        oracleCommand.Connection = this.oOracleConnection;
                        oracleCommand.Transaction = this.oOracleTransaction;
                        dataReader = (IDataReader)oracleCommand.ExecuteReader();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
            return dataReader;
        }
    }
}
