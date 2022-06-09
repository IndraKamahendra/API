using System;
using System.ComponentModel.DataAnnotations;


namespace API.Models
{
    public class Employee
    {
        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public Boolean IsDelete { get; set; }
        public Account Account { get; set; }
       
    }
    public enum Gender
    {
        Male,
        Female
    }
}
