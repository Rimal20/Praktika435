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
    public partial class EditProfilePage : ContentPage
    {
        User currentUser;
        public User User { get; set; }
        public EditProfilePage(User user)
        {
            InitializeComponent();
            currentUser = user;
            User = user;
            BindingContext = this;

            Title = "Редактировать профиль";
        }
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                // Сохранение изменений в базе данных SQLite
                await App.Database.SaveUserAsync(User);

                await DisplayAlert("Успех", "Профиль успешно обновлен", "OK");

                // Возвращение на предыдущую страницу
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось сохранить изменения: {ex.Message}", "OK");
            }
        }
    }
}