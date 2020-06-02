using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ChatApp.Models;
using ChatApp.Views;
using Core.Models;

namespace ChatApp.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        public ObservableCollection<Core.Models.User> Users { get; set; }
        public Command LoadItemsCommand { get; set; }

        public UsersViewModel()
        {
            Title = "Users";
            Users = new ObservableCollection<Core.Models.User>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadUsersCommand());
        }

        async Task ExecuteLoadUsersCommand()
        {
            IsBusy = true;

            try
            {
                Users.Clear();
                var users = await DataStore.GetAllUsers();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}