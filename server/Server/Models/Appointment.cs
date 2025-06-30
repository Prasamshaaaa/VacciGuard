namespace Server.Models
{
    public class Appointment
    {

        public int AppointmentId { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int VaccineId { get; set; }
        public Vaccine? Vaccine { get; set; }

        public DateTime AppointmentDate { get; set; }
        public bool DoseGiven { get; set; }
        public int DoseNumber { get; set; }

    }
}
