using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DAL
{
    public class EmployeeDao :BaseDao
    {
        public Employee GetEmployee (int id , string password)
        {
            string query = @"SELECT id,first_name,last_name,password,role FROM Employee
                WHERE id = @id AND password = @password";

            var parameters = new SqlParameter[]
            {
                    new SqlParameter("@id", SqlDbType.Int) {Value = id},
                    new SqlParameter("@password", SqlDbType.VarChar,  64) {Value = password}
            };

            DataTable table = ExecuteSelectQuery(query, parameters);

            if(table.Rows.Count == 0)
                return null;
            if (table.Rows.Count > 1)
                throw new DataException($"Multiple employees found with id {id}");

            return MapEmployee(table.Rows[0]);
        }

        private Employee MapEmployee(DataRow dr)
        {
            int id =(int)dr["id"];
            string firstName = (string)dr["first_name"];
            string lastName = (string)dr["last_name"];
            string password = (string)dr["password"];
            string roleString = (string)dr["role"];


            if (!Enum.TryParse<Role>(roleString, ignoreCase: true, out var role))
                throw new DataException($"Unrecognized role '{roleString}' for employee {id}");

            return new Employee(id, firstName, lastName, password, role);

        }
    }
    
    
}
