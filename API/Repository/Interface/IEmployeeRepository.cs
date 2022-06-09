using API.Models;
using System.Collections.Generic;


namespace API.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        Employee Get(string NIK);
        IEnumerable<Employee> GetSalary();
        Employee GetFirst(int Salary);
        Employee GetFirstOrDefault(int Salary);
        int Insert(Employee employee);
        int Update(Employee employee);
        int Delete(string NIK);
        Employee GetSingle(string FirstName);
        Employee GetSingleOrDefault(string FirstName);
    }  
}
