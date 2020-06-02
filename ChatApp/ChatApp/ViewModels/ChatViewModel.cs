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
    public class ChatViewModel : BaseViewModel
    {
        public ObservableCollection<MessageVM> Messages { get; set; }
        public Command LoadItemsCommand { get; set; }
        public User Companion { get; set; }

        public ChatViewModel(long userId)
        {
            Title = $"Loading";
            Companion = new User { Id = userId };

            Messages = new ObservableCollection<MessageVM>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadChatsCommand());
        }

        async Task ExecuteLoadChatsCommand()
        {
            IsBusy = true;

            try
            {
                var user = await DataStore.GetUserById(Companion.Id);
                Title = user.Username;

                Messages.Clear();
                var chat = await DataStore.GetChatById(Local.LocalUser.Id, Companion.Id);
                foreach (var message in chat.Messages)
                {
                    Messages.Add(new MessageVM(message));
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
        public async Task<bool> SendMessage(string text, long chatId, long userSenderId)
        {
            var message = await DataStore.SendMessage(chatId, userSenderId, text);
            Messages.Add(new MessageVM(message));

            return await Task.FromResult(true);
        }
    }

    public class MessageVM : Core.Models.Message
    {
        public User From { get; set; }
        public User To { get; set; }
        public string MessageFrom => $"@{From.Username} at {CreatedAt.ToShortTimeString()}";

        public MessageVM(Core.Models.Message message) : base(message) { }
    }
}