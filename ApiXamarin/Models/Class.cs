namespace ApiXamarin.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string TeacherFio { get; set; }
        public int Progress { get; set; }  // Добавляем новое поле для отслеживания прогресса
    }
}
