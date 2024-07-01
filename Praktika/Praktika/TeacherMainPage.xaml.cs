using SQLite;
using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Praktika.Models;
using Praktika.Services;

namespace Praktika
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherMainPage : ContentPage
    {
        private Class selectedLesson;
        User currentUser;
        private CsvService csvService;


        public TeacherMainPage(User user)
        {
            InitializeComponent();
            currentUser = user;
            WelcomeLabel.Text = $"Добро пожаловать, {user.FirstName} {user.LastName}!";
            LoadLessons();
            csvService = new CsvService();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadLessons();
        }

        private async void LoadLessons()
        {
            try
            {
                var lessons = await App.Database.GetClassesAsync(); // Получение данных из базы данных
                LessonsListView.ItemsSource = lessons;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Ошибка загрузки данных: {ex.Message}", "OK");
            }
        }
        private void LessonsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            selectedLesson = e.Item as Class;
            // Unselect item after tap
            ((ListView)sender).SelectedItem = null;
        }

        private async void OnAddLessonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddLessonPage(currentUser));
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchClassesPage(currentUser));
        }

        private async void OnProfileClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage(currentUser));
        }

        

        private async void OnChangeSelectedClicked(object sender, EventArgs e)
        {
            if (selectedLesson != null)
            {
                await Navigation.PushAsync(new EdirLessonPage(selectedLesson));
            }
            else
            {
                await DisplayAlert("Ошибка", "Выберите занятие для изменения", "OK");
            }
        }
        private async void OnDeleteSelectedClicked(object sender, EventArgs e)
        {
            if (selectedLesson != null)
            {
                bool result = await DisplayAlert("Подтвердить", $"Вы уверены, что хотите удалить занятие: {selectedLesson.Subject}?", "Да", "Отмена");
                if (result)
                {
                    await App.Database.DeleteClassAsync(selectedLesson);
                    await DisplayAlert("Успех", "Занятие удалено", "OK");
                    LoadLessons(); // Reload lessons after deletion
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Выберите занятие для удаления", "OK");
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
