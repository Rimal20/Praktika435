using Praktika.Models;
using SQLite;
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
    public partial class EdirLessonPage : ContentPage
    {
        private Class _lesson;
        
        public EdirLessonPage(Class lesson)
        {
            InitializeComponent();
            _lesson = lesson;
            SubjectEntry.Text = _lesson.Subject;
            DateEntry.Date = _lesson.Date;
            TeacherFioEntry.Text = _lesson.TeacherFio;
            ProgressSlider.Value = _lesson.Progress;
        }
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _lesson.Subject = SubjectEntry.Text;
            _lesson.Date = DateEntry.Date;
            _lesson.TeacherFio = TeacherFioEntry.Text;
            _lesson.Progress = (int)ProgressSlider.Value;

            await App.Database.UpdateClassAsync(_lesson);

            await DisplayAlert("Успех", "Занятие обновлено", "OK");
            await Navigation.PopAsync();
        }
    }
}