using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TivitProject_DL.ContextInfo;
using TivitProject_DL.InterfaceofRepos;
using TivitProject_EL.Entities;

namespace TivitProject_DL.ImplementationofRepos
{
    public class UserTivitRepo : Repository<UserTivit, long>, IUserTivitRepo
    {
        public UserTivitRepo(MyContext context) : base(context)
        {
        }
    }
}
