using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Web.Repos
{
    public interface IUsersRepo : IRepository<User>
    {
    }

    public class UsersRepo : Repository<User>, IUsersRepo
    {

    }
}
