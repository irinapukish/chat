using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Models;
using Core.Models;

namespace ChatApp.Services
{
    public class MockDataStore : IDataStore
    {
        List<User> Users;
        List<Chat> Chats;
        List<Message> Messages;
/*
        public MockDataStore()
        {
            Users = new List<User>()
            {
                new User
                {
                    Username = "iryna",
                    PasswordHash = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", //123
                    Bio = "Student WSIiZ, w60073",
                    Id = 1,
                    Avatar = "https://www.womenworking.com/wp-content/uploads/2016/11/4-Ways-to-Handle-a-Fake-Person.jpg"
                },
                new User
                {
                    Username = "billgates",
                    PasswordHash = "124",
                    Bio = "Student WSIiZ, w12345",
                    Id = 2,
                    Avatar = "https://upload.wikimedia.org/wikipedia/commons/a/a0/Bill_Gates_2018.jpg"
                },
                new User
                {
                    Username = "shepard",
                    PasswordHash = "215",
                    Bio = "ads spamer",
                    Id = 3,
                    Avatar = "https://cdn4.iconfinder.com/data/icons/election-voting/512/as_1354-512.png"
                },
            };

            Messages = new List<Message>
            {
                //User 1 to user 2
                new Message
                {
                    From = Users.First(),
                    ChatId = 1,
                    Id = 1,
                    Text = "Hi, how are you? Did you make the project?"
                },
                new Message
                {
                    From = Users.Skip(1).First(),
                    ChatId = 1,
                    Id = 2,
                    Text = "Hi, no, i don't know how to make simulator access for localhost api"
                },
                new Message
                {
                    From = Users.First(),
                    ChatId = 1,
                    Id = 3,
                    Text = "Its simple, just use python api instead"
                },

                //User 3 to user 2
                new Message
                {
                    From = Users.Last(),
                    ChatId = 2,
                    Id = 4,
                    Text = "Hi, do You wanna earn 2000$ in a few hours?"
                },
                new Message
                {
                    From = Users.Skip(1).First(),
                    ChatId = 2,
                    Id = 5,
                    Text = "lol I already a billionare"
                }
            };

            Chats = new List<Chat>()
            {
                new Chat
                {
                    Id = 1,
                    Members = new List<User>(Users.Take(2)),
                    Messages = Messages.Where(a => a.ChatId == 1).ToList()
                },
                   new Chat
                {
                    Id = 2,
                    Members = new List<User>(Users.Skip(1).Take(2)),
                    Messages = Messages.Where(a => a.ChatId == 2).ToList()
                }
            };
        }
        public async Task<User> Register(string username, string password)
        {
            //Check if user exists
            if (Users.Where(a => a.Username == username).Count() != 0)
                return null;

            //Create a new user
            var passwordHash = Helpers.HashPassword(password);
            var newUser = new User
            {
                Username = username,
                PasswordHash = passwordHash
            };

            //AddToDb
            Users.Add(newUser);

            return await Task.FromResult(newUser);
        }

        public async Task<User> Login(string username, string password)
        {
            //Register if user dont exists
            if (Users.Where(a => a.Username == username).Count() == 0)
            {
                var register = await Register(username, password);
                return await Task.FromResult(register);
            }

            //Compare passwords
            var passwordHash = Helpers.HashPassword(password);
            return await Task.FromResult(Users.FirstOrDefault(a => a.Username == username && a.PasswordHash == passwordHash));
        }

        public async Task<long> CreateChat(long userCreatorId, long withUserId)
        {
            //Check if chat exists
            var chats = Chats.Where(a => a.Members.Select(c => c.Id).Contains(userCreatorId) && a.Members.Select(c => c.Id).Contains(withUserId)).ToList();
            if (chats.Count > 0)
                return await Task.FromResult(chats.First().Id);

            Chats.Add(
                new Chat
                {
                    Members = Users.Where(a => a.Id == userCreatorId || a.Id == withUserId).ToList(),
                    Id = Chats.Last().Id + 1,
                    Messages = new List<Message>()
                });

            return await Task.FromResult(Chats.Last().Id);
        }

        public async Task<Message> SendMessage(long chatId, long userSenderId, string text)
        {
            //Check if chat exists
            var chat = Chats.FirstOrDefault(a => a.Id == chatId);
            if (chat == null)
                return null;

            var newMessage = new Message
            {
                Id = Messages.Last().Id + 1,
                ChatId = chatId,
                From = Users.First(a => a.Id == userSenderId),
                Text = text
            };

            Messages.Add(newMessage);

            chat.Messages.Add(Messages.Last());
            return await Task.FromResult(newMessage);
        }

        public async Task<IEnumerable<Chat>> GetChats(long userId)
        {
            var chats = Chats.Where(a => a.Members.Select(m => m.Id).Contains(userId)).ToList();

            return await Task.FromResult(chats);
        }

        public async Task<IEnumerable<Message>> GetMessages(long chatId)
        {
            var messages = Messages.Where(a => a.ChatId== chatId).ToList();

            return await Task.FromResult(messages);
        }*/

        public async Task<IEnumerable<User>> GetUsers(long userId)
        {
            var users = Users.Where(a => a.Id != userId).ToList();

            return await Task.FromResult(users);
        }

        public async Task<User> GetUser(long userId)
        {
            return await Task.FromResult(Users.FirstOrDefault(a => a.Id == userId));
        }

        public Task<User> Register(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendMessage(long chatId, long userSenderId, string text)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Chat>> GetChats(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<Chat> GetChatById(long userSenderId, long userReceiverId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}