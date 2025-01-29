using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
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

            // Encode token using Base64Url to prevent URL issues
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

            // Generate confirmation URL
            var confirmationUrl = $"https://localhost:7221/api/Auth/confirm-email?userId={user.Id}&token={encodedToken}";

            // HTML body with a button and a direct link option
            var htmlBody = $@"
            <p>Please confirm your email by clicking the button below:</p>
            <a href='{confirmationUrl}' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-align: center; text-decoration: none; display: inline-block; font-size: 16px; border-radius: 5px;'>
                Confirm Email
            </a> 
            <p>If the button doesn't work, you can also confirm your email by clicking the following link:</p>
            <p><a href='{confirmationUrl}'>{confirmationUrl}</a></p>";

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

        try
        {
            // Decode the token before using it
            var decodedTokenBytes = WebEncoders.Base64UrlDecode(token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

            return await _userManager.ConfirmEmailAsync(user, decodedToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Token decoding error: {ex.Message}");
            return IdentityResult.Failed(new IdentityError { Description = "Invalid token format." });
        }

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
