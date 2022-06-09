using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class GetRegisteredData
    {
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        public string University_name { get; set; }
    }
}
