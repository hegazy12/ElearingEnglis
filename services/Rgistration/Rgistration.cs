using ElearingEnglis.services.Rgistration.DTO;
using Microsoft.AspNetCore.Identity;
namespace ElearingEnglis.services.Rgistration;
public class Rgistration : IRgistration
{
    
    private readonly UserManager<IdentityUser> _userManager;

    // Inject UserManager via constructor
    public Rgistration(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> createNewUser(DTORgistration user)
    {
        IdentityUser newUser = new IdentityUser
        {
            UserName = user.Username,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = true,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(newUser, user.Password);
        return result;
    }
    
    public bool verifyEmail(string email)
    {
        throw new NotImplementedException();
    }

    public bool verifyPassword(string password)
    {
        throw new NotImplementedException();
    }

    public bool verifyPhone(string phone)
    {
        throw new NotImplementedException();
    }

    public bool verifyUser(string username)
    {
        throw new NotImplementedException();
    }
}
