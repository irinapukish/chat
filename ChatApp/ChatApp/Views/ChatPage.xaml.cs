using ChatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        ChatViewModel viewModel;

        public ChatPage(long userId)
        {
            InitializeComponent();

            BindingContext = viewModel = new ChatViewModel(userId);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Messages.Count == 0)
                viewModel.IsBusy = true;
        }
        async void SendMessage(object sender, EventArgs args)
        {
            var messageText = sendText.Text;

            if (messageText == null)
            {
                return;
            }

            await viewModel.SendMessage(messageText, Local.LocalUser.Id, viewModel.Companion.Id);

            sendText.Text = "";
            ItemsCollectionView.ScrollTo(viewModel.Messages.Count());
        }
    }
}