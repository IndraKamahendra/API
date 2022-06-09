using API.Context;
using API.Models;
using API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
/*using Degree = API.Models.Degree;
using Gender = API.Models.Gender;*/

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
      
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
        public int Insert(Register register)
        {
            Employee emp = new Employee();
            Account acc = new Account();
            Education edu = new Education();


            emp.NIK = GetAutoIncrement();
            emp.FirstName = register.FirstName;
            emp.LastName = register.LastName;
            emp.Phone = register.Phone;
            emp.Birthdate = register.Birthdate;
            emp.Salary = register.Salary;
            emp.Email = register.Email;

            emp.Gender = (Gender)Enum.Parse(typeof(Gender),register.Gender);

            //Account
            acc.Password = HashPassword(register.Password);

            //Account Role
            AccountRole accR = new AccountRole();
            accR.RoleId = "3";

            //Education
            University uni = context.Universities.Find(register.University_Id);
            edu.Degree = (Degree)Enum.Parse(typeof(Degree),register.Degree);
            edu.GPA = register.GPA;
            edu.University = uni;

            //profiling
            Profiling prof = new Profiling();
            prof.Education = edu;
            

            acc.Profiling = prof;
            emp.Account = acc;
            accR.AccountId = emp.NIK;

            context.Add(emp);
            context.Add(accR);
            var result = context.SaveChanges();
            return result;
        }
        public string GetAutoIncrement()
        {
            Employee employee = new Employee();
            var count = (from s in context.Employees orderby s.NIK select s.NIK).LastOrDefault();
            int last_id;
            if (count == null)
            {
                last_id = 1;
            }
            else
            {
                last_id = Convert.ToInt32(count.Substring(count.Length - 4)) + 1;
            }
            
            string new_count;
            if (last_id < 10)
            {
                new_count = "000" + last_id;
            }
            else if (last_id < 100)
            {
                new_count = "00" + last_id;
            }
            else if (last_id < 1000)
            {
                new_count = "0" + last_id;
            }
            else
            {
                new_count = last_id.ToString();
            }
            var tgl = DateTime.Now.ToString("MMddyyy") + new_count;
            return tgl;
        }

        public bool ValidateEmail(string email)
        {
            var emp = context.Employees.FirstOrDefault(emp => emp.Email == email);
            return emp != null;
        }
        public bool ValidatePhone(string phone)
        {
            var emp = context.Employees.FirstOrDefault(emp => emp.Phone == phone);
            return emp != null;
        }

        private string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }
        
        public bool ValidatePassword(string password, string correct)
        {
            return BCrypt.Net.BCrypt.Verify(password, correct);
        }

        public List<GetRegisteredData> GetRegisterdData()
        {
            List<GetRegisteredData> list = new List<GetRegisteredData>();

            foreach (var s in context.Employees.ToList())
            {
                var edu = (from e in context.Profilings
                           join p in context.Educations
                           on e.Education.Id equals p.Id
                           join u in context.Universities
                           on p.University.Id equals u.Id
                           where e.NIK == s.NIK
                           select new { 
                           p.Degree, p.GPA , u.Name}).FirstOrDefault();

                GetRegisteredData lists = new GetRegisteredData();
                lists.Fullname = s.FirstName + "" + s.LastName;
                lists.PhoneNumber = s.Phone;
                lists.Birthdate = s.Birthdate;
                lists.Gender = s.Gender.ToString();
                lists.Salary = s.Salary;
                lists.Email = s.Email;
                lists.Degree = edu.Degree.ToString();
                lists.GPA = edu.GPA;
                lists.University_name = edu.Name;
                list.Add(lists);
            }
            return list;
        }

    }
}
