using ChatApp.ViewModels;
using Core.Models;
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
    public partial class LoginRegisterPage : ContentPage
    {
        LoginRegisterViewModel viewModel;
        public LoginRegisterPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginRegisterViewModel();
        }
        async void LoginRegister_Clicked(object sender, EventArgs e)
        {
            var username = usernameInput.Text;
            var password = passwordInput.Text;

            if (username == null)
            {
                await  DisplayAlert("Username", "Username is Required", "ok");
                return;
            }
            else if (password == null)
            {
                await DisplayAlert("Password", "Password is Required", "ok");
                return;
            }

            //Log in
            var login = await viewModel.tryLoginOrRegister(username, password);
            if (login)
            {
                MessagingCenter.Send<Page>(this, "Loginned");
                await Navigation.PopModalAsync();
            }
            else
                await DisplayAlert("Wrong", "login or password", "ok");
        }
    }
}