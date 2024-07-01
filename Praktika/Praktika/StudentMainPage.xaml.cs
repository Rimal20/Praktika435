using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Praktika.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Praktika.Services;

namespace Praktika
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentMainPage : ContentPage
    {
        private SQLiteConnection _connection;
        private List<Class> _classes;
        private Class selectedLesson;
        User currentUser;
        private CsvService csvService;
        public StudentMainPage(User user)
        {
            InitializeComponent();
            WelcomeLabel.Text = $"Добро пожаловать, {user.FirstName} {user.LastName}!";
            LoadLessons();
            csvService = new CsvService();
        }
        private async void LoadLessons()
        {
            try
            {
                var lessons = await App.Database.GetClassesAsync(); // Получение данных из базы данных

                // Привязка данных к ListView
                LessonsListView1.ItemsSource = lessons;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Ошибка загрузки данных: {ex.Message}", "OK");
            }
        }
        private async void OnSearchClicked1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchClassesPage(currentUser));
        }

        private async void OnProfileClicked1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage1(currentUser));
        }
        private async void OnSaveToCsvClicked(object sender, EventArgs e)
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string filePath = Path.Combine(documentsPath, "lessons.csv");

                await csvService.SaveClassesToCsvAsync(filePath);

                await DisplayAlert("Успех", "Данные сохранены в CSV файл", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Ошибка сохранения CSV: {ex.Message}", "OK");
            }
        }
    }
}