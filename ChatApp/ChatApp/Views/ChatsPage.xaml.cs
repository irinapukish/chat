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
    public partial class ChatsPage : ContentPage
    {
        ChatsViewModel viewModel;

        public ChatsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ChatsViewModel();

            MessagingCenter.Subscribe<Page>(
                this,
                "Loginned",
                (sender) =>
                {
                    OnAppearing();
                });

            //if not logged
            if (Local.LocalUser == null)
                Navigation.PushModalAsync(new LoginRegisterPage());
        }
        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Core.Models.Chat)layout.BindingContext;

            await Navigation.PushAsync(new ChatPage(item.GetCompanion(Local.LocalUser.Id).Id));
        }

         protected override void OnAppearing()
        {
            if (Local.LocalUser != null)
            {
                base.OnAppearing();

                if (viewModel.Chats.Count == 0)
                    viewModel.IsBusy = true;
            }
        }
    }
}