
using ElearingEnglis.services.Rgistration.DTO;
using Microsoft.AspNetCore.Identity;
namespace ElearingEnglis.services.Rgistration;

public interface IRgistration
{
  public Task<IdentityResult> createNewUser(DTORgistration user);
  public bool verifyUser(string username);
  public bool verifyEmail(string email);
  public bool verifyPhone(string phone);
  public bool verifyPassword(string password);
}
