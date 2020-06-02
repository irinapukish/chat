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
    public class ChatsViewModel : BaseViewModel
    {
        public ObservableCollection<ChatVM> Chats { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ChatsViewModel()
        {
            Title = "Chats";
            Chats = new ObservableCollection<ChatVM>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadChatsCommand());
        }

        public async Task ExecuteLoadChatsCommand()
        {
            IsBusy = true;

            try
            {
                Chats.Clear();
                var chats = await DataStore.GetChats(Local.LocalUser.Id);
                foreach (var chat in chats)
                {
                    Chats.Add(new ChatVM(chat, Local.LocalUser.Id));
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
    public class ChatVM: Core.Models.Chat
    {
        public User Companion { get; set; }

        public ChatVM(Core.Models.Chat chat, long userId)
        {
            this.Members = chat.Members;
            this.Messages = chat.Messages;
            this.Companion = chat.Members.Where(a => a.Id != userId).FirstOrDefault();
        }
    }
}