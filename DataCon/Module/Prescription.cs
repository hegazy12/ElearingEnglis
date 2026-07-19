using System.ComponentModel.DataAnnotations.Schema;

namespace ElearingEnglis.DataCon.Module
{
    public class Prescription:BaseModule
    {
        public Guid PatientId { get; set; }
        public Doctor Doctor { get; set; }
        public List<Diagnos> Diagnosis { get; set; }  
        public string? Notes { get; set; }   
        [ForeignKey("Appointment")]
        public Guid AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        public ICollection<DrugItem> Items { get; set; }
       
    }
}
