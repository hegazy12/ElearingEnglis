using ElearingEnglis.services.Appoinment.DTO;
using ElearingEnglis.DataCon.Module;
using ElearingEnglis.DataCon;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ElearingEnglis.services.Doctor.DTO;
using NuGet.Protocol;
using ElearingEnglis.services.Patient.DTO;

namespace ElearingEnglis.services.Appoinment;
public class SAppoinment:IAppoinment
{

    private readonly DBCon _context;
    private readonly UserManager<IdentityUser>  _userManager;
    public SAppoinment(DBCon context,UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
 public Boolean Create(Guid Userid , CreateAppoinmentDTO dTO)
  {
  
    
    try {
       var Doctor = _context.doctors.Find(Guid.Parse(dTO.DoctorId));
       Console.WriteLine(Doctor.ToJson());
       var P      = _context.patients.Where(m=> m.Id == Guid.Parse(dTO.PatientId)).FirstOrDefault();
       Console.WriteLine(P.ToJson());
       Appointment _appointment = new Appointment()
          {
            AppointmentDate = dTO.DateAppoinment,
            Deposit=dTO.Deposit,
            Doctor= Doctor,
            Patient = P ,
            Notes = dTO.note
          };
          _appointment.Create(Userid);

          _context.appointments.Add(_appointment);
          _context.SaveChanges();
          
    }catch(Exception e)
    {
        Console.WriteLine("PPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");
        Console.WriteLine(e.InnerException.Message);
        return false;        
    }

    return true;
  }

public List<DTOAppoinment> GetPatientAppoinment(Guid idPatient)
{
    var patient = _context.patients.Find(idPatient);
    
    var Appoinments = _context.appointments.Where(m => m.Patient == patient).Include(m=> m.Doctor).ToList();
    
    List<DTOAppoinment> Result = new List<DTOAppoinment>();
    
    DataCon.Module.Doctor D = null;
    
    foreach (var i in Appoinments)
    {
      D = _context.doctors.Where(m=> m.Id == i.Doctor.Id).First();
      
      Result.Add(new DTOAppoinment (){
        AppoinmentDate = i.AppointmentDate ,
        Deposit = i.Deposit , 
        Doctor = new DTODoctor(){firstName = D.fristName , lastName = D.lastName , specialization =  D.specialization , ID = D.Id.ToString()},
        note = i.Notes,
        Id =i.Id
        });
    }
    return Result;
}

public List<DTOAppoinment1> GetDoctorAppoinment(Guid idDoctor , DateOnly AppointmentDate)
{
    var doctor = _context.doctors.Find(idDoctor);
    
    var Appoinments = _context.appointments.Where(m => m.Doctor == doctor && m.AppointmentDate == AppointmentDate).Include(m=> m.Patient).OrderBy(m => m.CreatedAt).ToList();
    
    List<DTOAppoinment1> Result = new List<DTOAppoinment1>();
    
    DataCon.Module.Patient P = null;
    
    foreach (var i in Appoinments)
    {
      P = _context.patients.Where(m=> m.Id == i.Patient.Id).First();
      
      Result.Add(new DTOAppoinment1 ()
      {
        AppoinmentDate = i.AppointmentDate ,
        Deposit = i.Deposit , 
        Patient = new DTOPatientCreat(){FristName = P.FristName , LastName= P.LastName , Phone = P.Phone },
        note = i.Notes,
        PatientId = i.PatientId,
        Id =i.Id
        });
    }

    return Result;
}
}
