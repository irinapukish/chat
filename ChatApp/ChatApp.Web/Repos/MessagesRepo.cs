using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Web.Repos
{
    public interface IMessagesRepo : IRepository<Message>
    {
    }

    public class MessagesRepo : Repository<Message>, IMessagesRepo
    {

    }
}
