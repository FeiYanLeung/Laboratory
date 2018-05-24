using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Laboratory.Core
{
    public sealed class EFExtend
    {
        private readonly DbConnection connection;

        public EFExtend() : this(AppConfig.MSSQL_CONNECTION_STRING) { }

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

        public void Merge<T>(DbContext context, T model) where T : class
        {
            var metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // Get the entity type from the model that maps to the CLR type
            var entityType = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                          .Single(e => objectItemCollection.GetClrType(e) == typeof(T));

            // Get the entity set that uses this entity type
            var entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                      .Single()
                      .EntitySets
                      .Single(s => s.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                          .Single()
                          .EntitySetMappings
                          .Single(s => s.EntitySet == entitySet);

            // Find the storage entity set (table) that the entity is mapped
            var tableEntitySet = mapping
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .StoreEntitySet;

            // Return the table name from the storage entity set
            var tableName = tableEntitySet.MetadataProperties["Table"].Value ?? tableEntitySet.Name;

            // Full properties
            var columns = mapping
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .PropertyMappings
                .OfType<ScalarPropertyMapping>()
                .Select(q => q.Column.Name);

            // Primary keys
            var keys = ((EntityTypeBase)tableEntitySet.MetadataProperties["ElementType"].Value)
                .KeyMembers
                .Select(q => q.Name);

            // Properties(not contains primary keys)
            var properties = columns.Where(column => !keys.Contains(column));

            var builder = new StringBuilder($" MERGE INTO [{tableName}] TAB ");
            builder.Append(" USING ( SELECT ");
            builder.Append(string.Join(", ", columns.Select(column => string.Format("@{0} AS [{0}]", column))));
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" ) AS TTAB ON ");
            builder.Append(string.Join(" AND ", keys.Select(key => string.Format("TAB.[{0}] = TTAB.[{0}]", key))));

            builder.Append(" WHEN MATCHED THEN UPDATE SET");
            builder.Append(string.Join(", ", properties.Select(property => string.Format("TAB.[{0}] = TTAB.[{0}]", property))));

            builder.Append(" WHEN NOT MATCHED THEN INSERT ( ");
            builder.Append(string.Join(", ", properties.Select(property => $"[{property}]")));
            builder.Append(" ) VALUES ( ");
            builder.Append(string.Join(", ", properties.Select(property => $"TTAB.[{property}]")));
            builder.Append(" ) OUTPUT INSERTED.ID; ");

            int inserted = context.Database.ExecuteSqlCommand(builder.ToString(), model);   // error! model is unknow parameters



            Console.WriteLine($" inserted:{inserted} ");
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
