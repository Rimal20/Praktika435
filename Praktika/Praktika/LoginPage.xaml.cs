using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praktika
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnLoginClicked(object sender, EventArgs e)
        {
            var user = await App.Database.GetUserAsync(UsernameEntry.Text);
            if (user != null && user.Password == PasswordEntry.Text)
            {
                if (user.Role == "teacher")
                {
                    await Navigation.PushAsync(new TeacherMainPage(user));
                }
                else if (user.Role == "student")
                {
                    await Navigation.PushAsync(new StudentMainPage(user));
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверное имя пользователя или пароль", "OK");
            }
        }

        async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
