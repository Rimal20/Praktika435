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
    public partial class SearchClassesPage : ContentPage
    {
        User currentUser;
        public List<Class> SearchResults { get; set; }

        public SearchClassesPage(User user)
        {
            InitializeComponent();
            currentUser = user;
            ResultsListView.ItemsSource = SearchResults;
        }
        private async void OnSearchClicked(object sender, EventArgs e)
        {
            string subject = TeacherFioEntry.Text.Trim(); // Получаем строку для поиска (можно добавить проверки на пустую строку)

            if (!string.IsNullOrEmpty(subject))
            {
                try
                {
                    List<Class> searchResults = await App.Database.SearchClassesAsync(subject);

                    ResultsListView.ItemsSource = searchResults;
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
    }
}