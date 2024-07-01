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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        async void OnRegisterClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = UsernameEntry.Text,
                Password = PasswordEntry.Text,
                FirstName = FirstNameEntry.Text,
                LastName = LastNameEntry.Text,
                MiddleName = MiddleNameEntry.Text,
                Role = RolePicker.SelectedItem.ToString()
            };

            var existingUser = await App.Database.GetUserAsync(user.Username);
            if (existingUser == null)
            {
                await App.Database.SaveUserAsync(user);
                await DisplayAlert("Успешно", "Пользователь зарегистрирован", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Ошибка", "Пользователь с таким именем уже существует", "OK");
            }
        }
    }
}