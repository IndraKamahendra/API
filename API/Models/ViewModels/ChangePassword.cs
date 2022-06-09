using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class ChangePassword
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string new_password { get; set; }
    }
}
