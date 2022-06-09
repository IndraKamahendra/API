using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    
    public class OldEmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext context;

        public OldEmployeeRepository(MyContext context)
        {
            this.context = context;
        }
        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }
        public Employee Get(string NIK)
        {
            return context.Employees.Find(NIK);
        }
        public IEnumerable<Employee> GetSalary()
        {
            return context.Employees.Where(s => s.Salary == 4000000).ToList();
        }
        public Employee GetFirst(int Salary)
        {
            try
            {
                return context.Employees.First(s => s.Salary == Salary);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Employee GetFirstOrDefault(int Salary)
        {
            return context.Employees.FirstOrDefault(s => s.Salary == Salary);
        }
        public int Insert(Employee employee)
        {     
            try
            {
                context.Employees.Add(employee);
                var result = context.SaveChanges();
                return result;
            }
            catch(Exception)
            {
                return 0;
            }
        }
        public int Update(Employee employee)
        {
            try
            {
                context.Entry(employee).State = EntityState.Modified;
                var result = context.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int Delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            if (entity == null)
            {
                return 0;
            }
            else
            {
                context.Remove(entity);
                var result = context.SaveChanges();
                return result;
            }

        }
        public Employee GetSingle(string FirstName)
        {
            try
            {
                return context.Employees.Single(s => s.FirstName == FirstName);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public Employee GetSingleOrDefault(string FirstName)
        {
            return context.Employees.SingleOrDefault(s => s.FirstName == FirstName);
        }
    }
}
