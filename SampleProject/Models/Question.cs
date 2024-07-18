namespace SampleProject.Models
{
    public class Question
    {
        public string QuestionText { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Content { get; set; } //to replace with questionText
    }
}
