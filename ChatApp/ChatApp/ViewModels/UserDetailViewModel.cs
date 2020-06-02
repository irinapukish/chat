using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ChatApp.Models;
using ChatApp.Views;
using Core.Models;
using System.Threading;

namespace ChatApp.ViewModels
{
    public class UserDetailViewModel : BaseViewModel
    {
        public Core.Models.User User { get; set; }
        public Command LoadUserCommand { get; set; }
        public string UserName { get; set; }
        public string UserBio { get; set; }
        public string UserAvatar { get; set; }

        public UserDetailViewModel(Core.Models.User user)
        {
            User = user;
            Title = User.Username;
            UserName = $"@{User.Username}";
            UserAvatar = User.Avatar;
            UserBio = $"{User.Bio}";
        }
    }
}