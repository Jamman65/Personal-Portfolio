//// Add this class to your Controllers namespace
//using Expense_Tracker.Models;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.IO;
//using System.Security.Claims;
//using System.Threading.Tasks;

//public class AccountController : Controller
//{
//    private readonly ApplicationDbContext _context;

//    public AccountController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    [HttpGet]
//    public IActionResult Login()
//    {
//        return View();
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Login(LoginViewModel model)
//    {
//        if (ModelState.IsValid)
//        {
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

//            if (user != null)
//            {
//                await Authenticate(user.Username);

//                return RedirectToAction("Index", "Dashboard");
//            }

//            ModelState.AddModelError("", "Invalid username or password.");
//        }

//        return View(model);
//    }

//    [HttpGet]
//    public IActionResult Register()
//    {
//        return View();
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Register(RegisterViewModel model, IFormFile profilePicture)
//    {
//        if (ModelState.IsValid)
//        {
//            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

//            if (existingUser == null)
//            {
//                var user = new User
//                {
//                    Name = model.Name,
//                    Username = model.Username,
//                    Password = model.Password
//                };

//                if (profilePicture != null && profilePicture.Length > 0)
//                {
//                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", profilePicture.FileName);

//                    using (var stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await profilePicture.CopyToAsync(stream);
//                    }

//                    user.ProfilePicture = Path.Combine("/uploads/", profilePicture.FileName);
//                }

//                _context.Users.Add(user);
//                await _context.SaveChangesAsync();

//                await Authenticate(user.Username);

//                return RedirectToAction("Index", "Dashboard");
//            }

//            ModelState.AddModelError("", "Username already exists.");
//        }

//        return View(model);
//    }

//    private async Task Authenticate(string username)
//    {
//        var claims = new List<Claim>
//        {
//            new Claim(ClaimTypes.Name, username),
//        };

//        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

//        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
//    }
//}
