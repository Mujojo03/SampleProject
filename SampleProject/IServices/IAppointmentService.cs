using SampleProject.Models;

namespace SampleProject.IServices
{
    public interface IAppointmentService
    {
        bool ScheduleAppointment(int patientId, int doctorId, DateTime appointmentDate);
        List<Appointment> GetAppointmentByDoctor(int doctorId);
        Patient GetPatientById(int patientId);
        bool IsWeekday(DateTime date);
    }
}
