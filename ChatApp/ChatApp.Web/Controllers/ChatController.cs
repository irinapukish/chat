using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Web.Interfaces;
using ChatApp.Web.Repos;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Web.Controllers
{
    [Route("api/chat")]     //https://localhost:44300/api/chat/getmessages?=1
    [ApiController]
    public class ChatController : ControllerBase, IApiInterface
    {
        private readonly IUsersRepo _userRepo;
        private readonly IMessagesRepo _messageRepo;

        public ChatController(
            IUsersRepo userrepo,
            IMessagesRepo messagerepo
            )
        {
            _userRepo = userrepo;
            _messageRepo = messagerepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("register")]
        public ActionResult<User> Register(string username, string password)
        {
            //Check if user exists
            var user = _userRepo.Set.FirstOrDefault(a => a.Username == username);
            if (user != null)
                return user;

            //Create a new user
            var newUser = new User
            {
                Username = username,
                PasswordHash = password
            };

            //AddToDb
            newUser = _userRepo.Add(newUser);

            return newUser;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("login")]
        public ActionResult<User> Login(string username, string password)
        {
            //Register if user dont exists
            if (!_userRepo.Set.Any(a => a.Username == username))
            {
                // Create a new user
                var newUser = new User
                {
                    Username = username,
                    PasswordHash = password
                };

                //AddToDb
                newUser = _userRepo.Add(newUser);
                return newUser;
            }

            //Compare login/password
            return _userRepo.Set.FirstOrDefault(a => a.Username == username && a.PasswordHash == password);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("sendmessage")]
        public ActionResult<Message> SendMessage(long userSenderId, long userReceiverId, string text)
        {
            if (userSenderId == 0 || userReceiverId == 0 || text == null)
                return null;

            var newMessage = new Message
            {
                FromUserId = userSenderId,
                ToUserId = userReceiverId,
                Text = text
            };

            var message = _messageRepo.Add(newMessage);

            return message;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("getchat")]
        public ActionResult<Chat> GetChatById(long userSenderId, long userReceiverId)
        {
            var chat = new Chat
            {
                Messages = _messageRepo.Set
                .Where(a =>
                a.FromUserId == userSenderId && a.ToUserId == userReceiverId ||
                a.FromUserId == userReceiverId && a.ToUserId == userSenderId
                )

                .OrderBy(a => a.CreatedAt).ToList(),
                Members = new List<User>() { _userRepo.Get(userSenderId), _userRepo.Get(userReceiverId) }
            };

            return chat;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("getchats")]
        public ActionResult<IEnumerable<Chat>> GetChats(long userId)
        {
            var user = _userRepo.Get(userId);
            var allUserMessages = _messageRepo.Set.Where(a =>
             a.FromUserId == userId || a.ToUserId == userId
                ).ToList();
            allUserMessages.ForEach(a => a.swapMembers(userId));

            var groupedMessages = allUserMessages.GroupBy(a => a.ToUserId).ToList();

            var chats = new List<Chat>();


            foreach (var a in groupedMessages)
            {
                var newChat = new Chat();
                newChat.Messages = a.ToList();
                var companion = _userRepo.Get(a.First().GetCompanion(userId));
                newChat.Members.Add(user);
                newChat.Members.Add(companion);

                chats.Add(newChat);
            }

            return chats;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("getusers")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return _userRepo.Set.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("getuser")]
        public ActionResult<User> GetUserById(long userId)
        {
            return _userRepo.Set.FirstOrDefault(a => a.Id == userId);
        }

        /*[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Item>> List()
        {
            return ItemRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Item> GetItem(string id)
        {
            Item item = ItemRepository.Get(id);

            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Item> Create([FromBody]Item item)
        {
            ItemRepository.Add(item);
            return CreatedAtAction(nameof(GetItem), new { item.Id }, item);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Edit([FromBody] Item item)
        {
            try
            {
                ItemRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while editing item");
            }
            return NoContent();
        }*/
    }
}