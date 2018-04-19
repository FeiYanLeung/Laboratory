using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace Laboratory.Core
{
    public sealed class EFExtend
    {
        private readonly DbConnection connection;

        public EFExtend() : this(AppConfig.SQLSERVER_CONNECTION_STRING) { }

        public EFExtend(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public static EFExtend Instance
        {
            get
            {
                return new EFExtend();
            }
        }

        public static EFExtend SetConnectionString(string connectionString)
        {
            return new EFExtend(connectionString);
        }

        public void Trans(Action<IDbCommand, IDbTransaction> invoke)
        {
            try
            {
                using (connection)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    using (var trans = connection.BeginTransaction())
                    {
                        using (var command = connection.CreateCommand())
                        {
                            try
                            {
                                command.Transaction = trans;
                                invoke(command, trans);
                            }
                            catch (DbException e)
                            {
                                trans.Rollback();
                                Debug.Fail(e.Message);
                                throw;
                            }
                        }
                    }
                }
            }
            catch (DbException e)
            {
                Debug.Fail(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                throw;
            }
        }

        public void Merge<T>(string tabName = null)
        {
            throw new NotImplementedException("wait for u.");
            tabName = tabName ?? typeof(T).Name;

            var builder = new StringBuilder($" MERGE INTO {tabName} ");
            builder.Append(" USING( ");

            builder.Append($" ) T{tabName} ");
            builder.Append(" ON T_NAME.[K] = NAME.K ");
            builder.Append(" WHEN MATCHED THEN UPDATE SET ");

            builder.Append(" WHEN NOT MATCHED THEN INSERT( ");

            builder.Append(" ) VALUES( ");

            builder.Append(" ) OUTPUT INSERTED.ID; ");
        }

        /// <summary>
        /// Dispose
        /// </summary>
        ~EFExtend()
        {
            Debug.Write("EFExtend Dispose~");
            //connection.Dispose();
        }
    }
}
