using ElearingEnglis.services.Appoinment.DTO;
using ElearingEnglis.DataCon.Module;
namespace ElearingEnglis.services.Appoinment;

public interface IAppoinment
{
   public Boolean Create(Guid Userid,CreateAppoinmentDTO dTO);
   public List<DTOAppoinment> GetPatientAppoinment(Guid idPatient);
   public List<DTOAppoinment1> GetDoctorAppoinment(Guid idDoctor,DateOnly AppointmentDate);
}
