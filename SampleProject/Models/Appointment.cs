namespace SampleProject.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppoinmentDate { get; set;}
        public string PatientName { get; set; }
         public string PatientDescription { get; set; }
    }
}
