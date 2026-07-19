using ElearingEnglis.services.Doctor.DTO;
using ElearingEnglis.services.Patient.DTO;

namespace ElearingEnglis.services.Appoinment.DTO;



public class DTOAppoinment0
{
    public Guid Id                      {get; set;}
    public DateOnly AppoinmentDate      {get; set;}
    public double Deposit               {get; set;}
    public  string? note                {get; set;}
}


public class DTOAppoinment : DTOAppoinment0
{
    public DTODoctor? Doctor  {get; set;}  
}

public class DTOAppoinment1 : DTOAppoinment0
{
    public DTOPatientCreat? Patient {get; set;}
    public Guid PatientId  {get; set;}
}

