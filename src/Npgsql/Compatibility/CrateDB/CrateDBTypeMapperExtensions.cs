using Npgsql.TypeHandlers;
using Npgsql.TypeMapping;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npgsql.Compatibility.CrateDB
{
    /// <summary>
    /// Implements extension methods for INpgsqlTypeMapper to setup type mappings for CrateDB.
    /// </summary>
    public static class CrateDBTypeMapperExtensions
    {
        /// <summary>
        /// Setup type mappings for CrateDB.
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static INpgsqlTypeMapper UseCrateDB(this INpgsqlTypeMapper mapper)
        {
            // Map CrateDB varchar type to the Npgsql TextHandler.
            mapper.AddMapping(new NpgsqlTypeMappingBuilder
            {
                PgTypeName = "varchar",
                NpgsqlDbType = NpgsqlDbType.Varchar,
                DbTypes = new[] { DbType.String, DbType.StringFixedLength, DbType.AnsiString, DbType.AnsiStringFixedLength },
                ClrTypes = new[] { typeof(string), typeof(char[]), typeof(char) },
                InferredDbType = DbType.String,
                TypeHandlerFactory = new TextHandlerFactory()
            }
            .Build());

            // Map CrateDB timestampz type to the CrateDBTimestampHandler.
            mapper.AddMapping(new NpgsqlTypeMappingBuilder
            {
                PgTypeName = "timestampz",
                NpgsqlDbType = NpgsqlDbType.TimestampTz,
                DbTypes = new[] { DbType.DateTime },
                ClrTypes = new[] { typeof(DateTime) },
                TypeHandlerFactory = new CrateDBTimestampHandlerFactory()
            }
            .Build());

            return mapper;
        }
    }
}
