using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Web.Interfaces
{
    public interface IApiInterface
    {
       // ActionResult<User> Register(string username, string password);
        ActionResult<User> Login(string username, string password);
        ActionResult<Message> SendMessage(long userSenderId, long userReceiverId, string text);
        ActionResult<IEnumerable<Chat>> GetChats(long userId);
        ActionResult<Chat> GetChatById(long userSenderId, long userReceiverId);
        ActionResult<User> GetUserById(long userId);
        ActionResult<IEnumerable<User>> GetAllUsers();
       // ActionResult<IEnumerable<Message>> GetMessages(long chatId);
    }
}
