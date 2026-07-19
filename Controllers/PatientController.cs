using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ElearingEnglis.services.Patient;
using ElearingEnglis.services.Patient.DTO;
using System.Security.Claims;

namespace ElearingEnglis.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(Roles = "Admin,User")]
public class PatientController : ControllerBase
{
    IPatient _IPatient;
    public PatientController(IPatient patient)
    {
        _IPatient = patient;
    }
    
    [HttpPost]
    public bool Create(DTOPatientCreat creat)
    {
       var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid userid = Guid.Parse(userIdStr);
        return _IPatient.createPatient(userid,creat);
    }
    
    [HttpGet("{numberpage:int}")]
    public List<DTOPatien> GetPatiensInPage(int numberpage)
    {
        return _IPatient.GetDTOPatiens(numberpage);
    }
    
    [HttpGet("{id:guid}")]
    public DTOPatien GetPatien(Guid id)
    {
        return _IPatient.getPatient(id);
    }
    
}
