using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
