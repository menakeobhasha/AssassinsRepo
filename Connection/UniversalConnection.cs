using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class UniversalConnection: IDisposable
    {
        private string connectionString = string.Empty;
        private bool disposed;
        private bool isException;
        private SqlConnection oSqlConnection;
        private SqlTransaction oSqlTransaction;
        private List<Parameter> parameterCollection;
        private string _outParameter;

        public UniversalConnection(string connectionString)
        {
            this.connectionString = connectionString;
            this.OpenConnection();
            this.BeginTransaction();
            this.parameterCollection = new List<Parameter>();
            this._outParameter = string.Empty;
            this.disposed = false;
        }

        ~UniversalConnection()
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

        public string OutParameter
        {
            get
            {
                return this._outParameter;
            }
            set
            {
                this._outParameter = value;
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
            this.oSqlTransaction = this.oSqlConnection.BeginTransaction(isolationLevel);
        }

        private void BeginTransaction()
        {
            this.oSqlTransaction = this.oSqlConnection.BeginTransaction();
        }

        private void CloseConnection()
        {
            if (this.oSqlConnection.State == ConnectionState.Closed)
                return;
            this.oSqlConnection.Close();
        }

        private void CommitTransaction()
        {
            this.oSqlTransaction.Commit();
        }

        private void OpenConnection()
        {
            if (this.oSqlConnection == null)
                this.oSqlConnection = new SqlConnection(this.connectionString);
            this.oSqlConnection.Open();
        }

        private void RollbackTransaction()
        {
            if (this.oSqlTransaction == null)
                return;
            this.oSqlTransaction.Rollback();
        }

        private List<SqlParameter> GetSqlParameters(List<Parameter> parameters)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            string empty = string.Empty;
            foreach (Parameter parameter in parameters)
            {
                SqlParameter sqlParameter = new SqlParameter(parameter.Name, parameter.Value);
                sqlParameterList.Add(sqlParameter);
            }
            return sqlParameterList;
        }

        public DataSet ExecuteDataSet()
        {
            DataSet dataSet = new DataSet();
            string commandText = this.CommandText;
            try
            {
                string str = commandText.Replace('?', '@');
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandTimeout = Common.CommandTimeout;
                sqlCommand.CommandText = str;
                if (this.Parameters.Count > 0)
                    sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                sqlCommand.Connection = this.oSqlConnection;
                sqlCommand.Transaction = this.oSqlTransaction;
                new SqlDataAdapter() { SelectCommand = sqlCommand }.Fill(dataSet);
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
            string commandText = this.CommandText;
            try
            {
                string str = commandText.Replace('?', '@');
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandTimeout = Common.CommandTimeout;
                sqlCommand.CommandText = str;
                if (this.Parameters.Count > 0)
                    sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                sqlCommand.Connection = this.oSqlConnection;
                sqlCommand.Transaction = this.oSqlTransaction;
                new SqlDataAdapter() { SelectCommand = sqlCommand }.Fill(dataSet, tableName);
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
            string commandText = this.CommandText;
            try
            {
                string str = commandText.Replace('?', '@');
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandTimeout = Common.CommandTimeout;
                sqlCommand.CommandText = str;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (this.Parameters.Count > 0)
                    sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                sqlCommand.Connection = this.oSqlConnection;
                sqlCommand.Transaction = this.oSqlTransaction;
                return (IDataReader)sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
        }

        public int ExecuteQuery()
        {
            string commandText = this.CommandText;
            try
            {
                string str = commandText.Replace('?', '@');
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandTimeout = Common.CommandTimeout;
                sqlCommand.CommandText = str;
                sqlCommand.Connection = this.oSqlConnection;
                sqlCommand.Transaction = this.oSqlTransaction;
                if (this.Parameters.Count > 0)
                    sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                return sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
        }

        public IDataReader ExecuteReader()
        {
            string commandText = this.CommandText;
            try
            {
                string str = commandText.Replace('?', '@');
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandTimeout = Common.CommandTimeout;
                sqlCommand.CommandText = str;
                sqlCommand.Connection = this.oSqlConnection;
                sqlCommand.Transaction = this.oSqlTransaction;
                if (this.Parameters.Count > 0)
                    sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                return (IDataReader)sqlCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
        }

        public object ExecuteScalar()
        {
            string commandText = this.CommandText;
            try
            {
                string str = commandText.Replace('?', '@');
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandTimeout = Common.CommandTimeout;
                sqlCommand.CommandText = str;
                if (this.Parameters.Count > 0)
                    sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                sqlCommand.Connection = this.oSqlConnection;
                sqlCommand.Transaction = this.oSqlTransaction;
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
        }

        public string ExecuteProcedureWithOutput()
        {
            string commandText = this.CommandText;
            SqlParameter sqlParameter = (SqlParameter)null;
            string empty = string.Empty;
            try
            {
                string str = commandText.Replace('?', '@');
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandTimeout = Common.CommandTimeout;
                sqlCommand.CommandText = str;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                if (this.Parameters.Count > 0)
                    sqlCommand.Parameters.AddRange(this.GetSqlParameters(this.Parameters).ToArray());
                if (!string.IsNullOrEmpty(this.OutParameter))
                {
                    string parameterName = "@" + this.OutParameter;
                    sqlParameter = sqlCommand.Parameters.Add(parameterName, SqlDbType.VarChar, 1000);
                    sqlParameter.Direction = ParameterDirection.Output;
                }
                sqlCommand.Connection = this.oSqlConnection;
                sqlCommand.Transaction = this.oSqlTransaction;
                sqlCommand.ExecuteNonQuery();
                return sqlParameter.Value.ToString();
            }
            catch (Exception ex)
            {
                this.isException = true;
                List<Parameter> parameters = this.Parameters;
                throw ex;
                //throw new Exception(CustomException.RaiseError(ex, parameters));
            }
        }
    }
}
