using Praktika.Models;
using SQLite;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Praktika
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLessonPage : ContentPage
    {
        //private SQLiteConnection _database;
        private readonly User currentUser;

        public AddLessonPage(User user)
        {
            InitializeComponent();
            //_database = database;
            currentUser = user;
        }

        private async void OnAddLessonClicked(object sender, EventArgs e)
        {
            if (App.Database == null)
            {
                // Handle the case where database is not initialized
                await DisplayAlert("Ошибка", "База данных не инициализирована", "OK");
                return;
            }

            string subject = SubjectEntry.Text;
            DateTime date = DatePicker.Date.Add(TimePicker.Time);
            string teacherFio = TeacherFioEntry.Text;
            int progress;

            if (string.IsNullOrWhiteSpace(subject) ||
                string.IsNullOrWhiteSpace(teacherFio) ||
                !int.TryParse(ProgressEntry.Text, out progress))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите все данные правильно", "OK");
                return;
            }

            var newLesson = new Class
            {
                Subject = subject,
                Date = date,
                TeacherFio = teacherFio,
                Progress = progress
            };

            // Ensure App.Database is not null before calling InsertAsync
            if (App.Database != null)
            {
                await App.Database.InsertAsync(newLesson);

                await DisplayAlert("Успех", "Занятие добавлено", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Ошибка", "База данных не доступна", "OK");
            }
        }
    }
}
