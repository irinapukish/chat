using ChatApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]

    public partial class UsersPage : ContentPage
    {
        UsersViewModel viewModel;
        public UsersPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UsersViewModel();

            //if not logged
            if (Local.LocalUser == null)
                Navigation.PushModalAsync(new LoginRegisterPage());
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Core.Models.User)layout.BindingContext;
            await Navigation.PushAsync(new UserDetailPage(item));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Users.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}