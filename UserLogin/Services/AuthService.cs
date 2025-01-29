using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using UserLogin.Data.Models;
using UserLogin.Services;

public class AuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly EmailService _emailService;

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService, EmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterModel model)
    {
        var user = new IdentityUser { UserName = model.Username, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // Generate email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Generate confirmation URL (you need to provide the actual URL here)
            var confirmationUrl = $"https://localhost:7221/api/Auth/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            // HTML body with a button
            var htmlBody = $@"
            <p>Please confirm your email by clicking the button below:</p>
            <a href='{confirmationUrl}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; font-size: 16px; border-radius: 5px;'>
                Confirm Email
            </a> ";

            // Send the confirmation email using the EmailService
            await _emailService.SendEmailAsync(user.Email, "Confirm your email", htmlBody, true);
        }

        return result;
    }


    public async Task<SignInResult> LoginAsync(LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Identifier) ?? await _userManager.FindByEmailAsync(model.Identifier);
        if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            return SignInResult.Failed;

        return await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Invalid user ID." });

        return await _userManager.ConfirmEmailAsync(user, token);
    }

    public async Task<string> GenerateJwtTokenAsync(IdentityUser user)
    {
        return await _tokenService.CreateTokenAsync(user);
    }

    public async Task<IdentityUser> GetUserByNameAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }
}
