namespace SampleProject.Models
{
    public class ScheduleAppointmentRequest
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
