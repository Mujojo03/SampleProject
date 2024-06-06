using SampleProject.IServices;
using SampleProject.Models;

namespace SampleProject.Services
{
    public class AppointmentService : IAppointmentService
    {
        //lists to store the patients, doctors and appointments
        private readonly List<Patient> _patients = new List<Patient>();
        private readonly List<Doctor> _doctors = new List<Doctor>();
        private readonly List<Appointment> _appointments = new List<Appointment>();

        //schedule an appointment if the date is weekday
        public bool ScheduleAppointment(int patientId, int doctorId, DateTime appointmentDate)
        {
            if (!IsWeekday(appointmentDate))
            {
                return false;
            }

            var appointment = new Appointment
            {
                PatientId = patientId,
                DoctorId = doctorId,
                Id = _appointments.Count + 1,
                AppoinmentDate = appointmentDate
            };

            _appointments.Add(appointment); //add tthe new appointment to the list
            return true;
        }

        //to retrieve patient info
        public Patient GetPatientById(int  patientId)
        {
            return _patients.FirstOrDefault(p => p.Id == patientId); //to return patient with specified id
        }

        public bool IsWeekday(DateTime date)
        {
            //return true is date is weekday
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }

        //to retrieve all appointments for a doctor
        public List<Appointment> GetAppointmentByDoctor(int doctorId)
        {
            return _appointments.Where(a => a.DoctorId == doctorId).ToList();
        }
    }
}

