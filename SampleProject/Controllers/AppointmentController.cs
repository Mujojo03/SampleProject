using Microsoft.AspNetCore.Mvc;
using SampleProject.IServices;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : Controller
    {  
        //global vriable
        private readonly IAppointmentService _appointmentService;

        //constructor class with DI for appoinment service
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("Schedule")]
        public IActionResult ScheduleAppointment([FromBody] ScheduleAppointmentRequest request)
        {
            if (!_appointmentService.IsWeekday(request.AppointmentDate))
            {
                return BadRequest(new { message = "Appointment Can Only Be Scheduled on Weekdays." });
            }
            var success = _appointmentService.ScheduleAppointment(request.PatientId, request.DoctorId, request.AppointmentDate);

            if (!success)
            {
                return BadRequest(new { message = "Failed to Schedule Appointment" });
            }
            return Ok(new {message = "Appointment Scheduled Successfully"});
        }

        [HttpGet("doctor/ {doctorId}")]
        public IActionResult GetAppointmentsByDoctor(int doctorId)
        {
            var appointments = _appointmentService.GetAppointmentByDoctor(doctorId);
            return Ok(appointments);
        }

        [HttpGet("patient/{patientId}")]
        public IActionResult GetPatientById(int patientId)
        {
            var patient = _appointmentService.GetPatientById(patientId);
            if(patient == null)
            {
                return NotFound(new { message = "Patient Not Found." });
            }
            return Ok(patient);
        }
    }
}
