using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ElearingEnglis.services.Appoinment;
using ElearingEnglis.services.Appoinment.DTO;
using System.Security.Claims;

namespace ElearingEnglis.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(Roles = "Admin,User")]
public class AppointmentController : ControllerBase
{
    private readonly IAppoinment _SAppoinment;
   
    public AppointmentController(IAppoinment appoinment )
    {
        _SAppoinment = appoinment;
    }

   [HttpPost]
   public Boolean Creat(CreateAppoinmentDTO DTO)
   {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Guid userid = Guid.Parse(userIdStr);
        return  _SAppoinment.Create(userid,DTO);
   }
 
   [HttpGet("{Patient:guid}")]
    public List<DTOAppoinment> GetDTOAppoinments(Guid Patient)
    {
        return _SAppoinment.GetPatientAppoinment(Patient);
    }

    [HttpGet("{Doctor:guid}/{AppointmentDate}")]
    public List<DTOAppoinment1> GetDoctorAppoinments(Guid Doctor, DateOnly AppointmentDate)
    {
        return _SAppoinment.GetDoctorAppoinment(Doctor, AppointmentDate);
    }
    
}
