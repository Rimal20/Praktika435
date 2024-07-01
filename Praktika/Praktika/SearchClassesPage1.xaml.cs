using Praktika.Models;
using Praktika.Services;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praktika
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchClassesPage1 : ContentPage
    {
        User currentUser;
        public List<Class> SearchResults { get; set; }
        private CsvService csvService;
        public SearchClassesPage1(User user)
        {
            InitializeComponent();
            currentUser = user;
            ResultsListView1.ItemsSource = SearchResults;
            csvService = new CsvService();
        }
        private async void OnSearchClicked(object sender, EventArgs e)
        {
            string subject = TeacherFioEntry.Text.Trim(); // Получаем строку для поиска (можно добавить проверки на пустую строку)

            if (!string.IsNullOrEmpty(subject))
            {
                try
                {
                    List<Class> searchResults = await App.Database.SearchClassesAsync(subject);

                    ResultsListView1.ItemsSource = searchResults;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", $"Ошибка поиска занятий: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите предмет для поиска", "OK");
            }
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