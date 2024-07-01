using Praktika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praktika
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage1 : ContentPage
    {
        public User CurrentUser { get; set; }
        User currentUser;
        public ProfilePage1(User user)
        {
            InitializeComponent();
            currentUser = user;
            CurrentUser = currentUser;
            BindingContext = this;
            

            Title = "Профиль студента";
        }
        private async void OnEditProfileClicked(object sender, EventArgs e)
        {
            // Реализация перехода на страницу редактирования профиля
            await Navigation.PushAsync(new EditProfilePage(CurrentUser));
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            // Реализация выхода из учетной записи (выхода из приложения или переход на страницу входа)
            // Здесь может быть ваша логика выхода из аккаунта
            await Navigation.PopToRootAsync(); // Возврат на главную страницу
        }
    }
}