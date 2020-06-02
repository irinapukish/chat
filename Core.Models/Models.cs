using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Core.Models
{
    public class BaseDBClass
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Chat
    {
        public virtual ICollection<User> Members { get; set; } = new List<User>();
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

        public User GetCompanion(long userId)
        {
            return Members.Where(a => a.Id != userId).FirstOrDefault();
        }
    }

    public class User : BaseDBClass
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
    }
    public class Message : BaseDBClass
    {
        public string Text { get; set; }

        public long FromUserId { get; set; }
       /* [ForeignKey(nameof(FromUserId))]
        public virtual User From { get; set; }*/

        public long ToUserId { get; set; }
        /*[ForeignKey(nameof(ToUserId))]
        public virtual User To { get; set; }*/

        public bool IsMembers(long usr1, long usr2)
        {
            return (usr1 == FromUserId && usr2 == ToUserId || usr1 == ToUserId && usr2 == FromUserId);
        }

        public bool IsMember(long usrId)
        {
            return (usrId == FromUserId || usrId == ToUserId);
        }

        public long GetCompanion(long usrId)
        {
            return usrId == FromUserId ? ToUserId : FromUserId;
        }

        public void swapMembers(long usrId)
        {
            if (usrId != FromUserId)
            {
                (FromUserId, ToUserId) = (ToUserId, FromUserId);
            }
        }

        public Message() { }
        public Message(Message message)
        {
            this.Id = message.Id;
            this.CreatedAt = message.CreatedAt;
            this.Text = message.Text;
            this.FromUserId = message.FromUserId;
            this.ToUserId = message.ToUserId;
        }
    }

    /*   public class Message : BaseDBClass
       {
           public string Text { get; set; }
           public long ChatId { get; set; }

           public long FromUserId { get; set; }
           [ForeignKey(nameof(FromUserId))]
           public virtual User From { get; set; }

          
       }*/
}
