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
    public partial class UserDetailPage : ContentPage
    {
        UserDetailViewModel viewModel;
        public UserDetailPage(Core.Models.User user)
        {
            InitializeComponent();

            BindingContext = viewModel = new UserDetailViewModel(user);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.User.Avatar == null)
                viewModel.IsBusy = true;
        }
        async void CreateChatClick(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ChatPage(viewModel.User.Id));
        }
    }
}