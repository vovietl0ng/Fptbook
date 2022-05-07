using Fptbook.Models;
using Fptbook.Models.EF;
using Fptbook.Models.Entity;
using Fptbook.ViewModel.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fptbook.Controllers;
public class LoginsController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IConfiguration _config;
    private readonly FptDbContext _context;
    public LoginsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        RoleManager<AppRole> roleManager, IConfiguration config, FptDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _config = config;
        _context = context;

    }
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null) return View();
        await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
        
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new[]
        {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.StreetAddress,user.Address),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
                new Claim(ClaimTypes.Name,request.UserName)
            };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Tokens:Issuer"],
            _config["Tokens:Issuer"],
            claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds);

        var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);


        var userPrincipal = this.ValidateToken(tokenResult);
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            IsPersistent = true
        };
        HttpContext.Session.SetString("Token", tokenResult);
        await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    authProperties);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Remove("Token");
        return RedirectToAction("Login", "Logins");
    }

    [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user != null)
        {
            return View();
        }
        user = new AppUser()
        {
            FullName = request.FullName,
            Email = request.Email,
            Address = request.Address,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        await _userManager.AddToRoleAsync(user, "customer");
        if (result.Succeeded)
        {
            return RedirectToAction("Login", "Logins");
        }
        return View();
    }

    [HttpGet]
    public IActionResult RegisterStore()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> RegisterStore(RegisterStoreRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user != null)
        {
            return View();
        }
        user = new AppUser()
        {
            FullName = request.FullName,
            Email = request.Email,
            Address = request.Address,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        await _userManager.AddToRoleAsync(user, "customer");
        var store = new Store()
        {
            Address = request.Address,
            Name = request.Name,
            Slogan = request.Slogan,
            UserId = user.Id
        };
        _context.Stores.Add(store);
        _context.SaveChanges();
        if (result.Succeeded)
        {
            return RedirectToAction("Login", "Logins");
        }
        return View();
    }



    private ClaimsPrincipal ValidateToken(string jwtToken)
    {
        IdentityModelEventSource.ShowPII = true;

        SecurityToken validatedToken;
        TokenValidationParameters validationParameters = new TokenValidationParameters();

        validationParameters.ValidateLifetime = true;

        validationParameters.ValidAudience = _config["Tokens:Issuer"];
        validationParameters.ValidIssuer = _config["Tokens:Issuer"];
        validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

        ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

        return principal;
    }
}
