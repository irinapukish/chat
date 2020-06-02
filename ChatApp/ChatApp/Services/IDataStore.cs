using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public interface IDataStore
    {
        Task<User> Register(string username, string password);
        Task<User> Login(string username, string password);
        Task<Message> SendMessage(long userSenderId, long userReceiverId, string text);
        Task<IEnumerable<Chat>> GetChats(long userId);
        Task<Chat> GetChatById(long userSenderId, long userReceiverId);
        Task<User> GetUserById(long userId);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
