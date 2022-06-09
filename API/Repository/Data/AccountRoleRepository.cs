using API.Context;
using API.Models;
using API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, string>
    {
        private readonly MyContext context;

        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int SignManager(SignManager signManager)
        {
            var sign = (from e in context.AccountRoles
                        where e.AccountId == signManager.NIK && e.RoleId == "2"
                        select e).FirstOrDefault();

            if (sign == null)
            {
                AccountRole accountRole = new AccountRole();
                accountRole.AccountId = signManager.NIK;
                accountRole.RoleId = "2";
                context.AccountRoles.Add(accountRole);
                context.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
