using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Praktika.Models;

namespace Praktika.Services
{
    public class CsvService
    {
        public async Task SaveToCsvAsync(List<Class> classes, string filePath)
        {
            try
            {
                using (var streamWriter = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    await streamWriter.WriteLineAsync("Subject,Date,TeacherFio,Progress");

                    foreach (var lesson in classes)
                    {
                        await streamWriter.WriteLineAsync($"{lesson.Subject},{lesson.Date},{lesson.TeacherFio},{lesson.Progress}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения CSV: {ex.Message}");
            }
        }

        // Новый метод для сохранения занятий
        public async Task SaveClassesToCsvAsync(string filePath)
        {
            try
            {
                var lessons = await App.Database.GetClassesAsync();
                using (var streamWriter = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    await streamWriter.WriteLineAsync("Subject,Date,TeacherFio,Progress");

                    foreach (var lesson in lessons)
                    {
                        await streamWriter.WriteLineAsync($"{lesson.Subject},{lesson.Date},{lesson.TeacherFio},{lesson.Progress}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения занятий в CSV: {ex.Message}");
            }
        }
    }
}
