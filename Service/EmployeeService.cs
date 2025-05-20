using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using System.Security.Cryptography;

namespace Service
{
    public class EmployeeService
    {
        private EmployeeDao employeeDao;
        public EmployeeService()
        {
            employeeDao = new EmployeeDao();
        }

        public Employee Authenticate(int id , string password)
        {
            string hashed = HashPassword(password);

            return employeeDao.GetEmployee(id, hashed);
        }

        private string HashPassword(string password)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes   = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(bytes);

            var sb = new StringBuilder(hashBytes.Length *2);
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
