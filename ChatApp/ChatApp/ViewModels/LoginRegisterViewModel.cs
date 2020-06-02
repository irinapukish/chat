using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ChatApp.Models;
using ChatApp.Views;
using Core.Models;
using System.Linq;

namespace ChatApp.ViewModels
{
    public class LoginRegisterViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; set; }

        public LoginRegisterViewModel()
        {
            Title = "Login";
        }

        public async Task<bool> tryLoginOrRegister(string name, string password)
        { 
            var loginnedUser = await DataStore.Login(name, password);

            Local.LocalUser = loginnedUser;

            return loginnedUser != null;
        }
    }
   
}