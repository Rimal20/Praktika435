using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praktika.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praktika
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        User currentUser;
        public User CurrentUser { get; set; }
        public ProfilePage(User user)
        {
            InitializeComponent();
            currentUser = user;
            CurrentUser = currentUser;
            BindingContext = this;

        }
        private async void OnEditProfileClicked(object sender, EventArgs e)
        {
            // Реализация редактирования профиля, например, переход на страницу редактирования
            await Navigation.PushAsync(new EditProfilePage(CurrentUser));
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            // Реализация выхода из учетной записи, например, переход на страницу входа
            await Navigation.PushAsync(new LoginPage());
        }
    }
}