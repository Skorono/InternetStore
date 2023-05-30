using InternetStore.ModelDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

namespace InternetStore.Controls
{
    static class BaseProvider
    {
        private static InternetStoreContext dbContext = null!;

        public static InternetStoreContext DbContext
        {
            get => dbContext;

            private set { }
        }

        static BaseProvider()
        {
            Update();
        }

        /// <summary>
        /// Provide to call stored procedure from DB
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int CallStoredProcedureByName(string procedure, params SqlParameter[] parameters)
        {
            StringBuilder sqlRequest = new StringBuilder($"EXEC ").AppendFormat(@"{0} ", procedure);
            foreach (SqlParameter parameter in parameters)
            {
                sqlRequest.AppendFormat("@{0}", parameter.ParameterName);
                if (parameters.LastOrDefault() != parameter)
                    sqlRequest.Append(", ");
            }
            return dbContext.Database.ExecuteSqlRaw(sqlRequest.ToString(), parameters);
        }

        public static void Update()
        {
            dbContext = new InternetStoreContext();
        }

        /*public static List<T> GetView<T>(string viewName)
        {
            return dbContext.Database.SqlQuery<T>($"SELECT * from dbo.{viewName}").ToList();
        }*/
    }
}
