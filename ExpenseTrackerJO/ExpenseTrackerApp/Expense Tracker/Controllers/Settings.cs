//using Microsoft.AspNetCore.Mvc;
//using Expense_Tracker.Models;
//using System.Threading.Tasks;

//namespace Expense_Tracker.Controllers
//{
//    public class SettingsController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public SettingsController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Settings
//        public IActionResult Index()
//        {
//            SettingsViewModel viewModel = new SettingsViewModel();
//            viewModel.Name = "Default";

//            // Retrieve user's name from the database and pass it to the view
//            var user = _context.Users.FirstOrDefault(); // Replace with your own logic to retrieve the user

//            if (user != null)
//            {
//                viewModel.Name = user.Name;
//            }

//            return View(viewModel);
//        }



//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> UpdateName(string name)
//        {
//            if (!string.IsNullOrWhiteSpace(name))
//            {
//                // Update the user's name in the database
//                var user = _context.Users.FirstOrDefault(); // Replace with your own logic to retrieve the user

//                if (user != null)
//                {
//                    user.Name = name;
//                    await _context.SaveChangesAsync();
//                }
//            }

//            return RedirectToAction("Index");
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> CreateUser(string name)
//        {
//            if (!string.IsNullOrWhiteSpace(name))
//            {
//                // Create a new user object
//                var newUser = new SettingsViewModel { Name = name };

//                // Add the new user to the database
//                _context..Add(newUser);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToAction("Index");
//        }



//        // POST: Settings/UpdateProfilePicture
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> UpdateProfilePicture(string profilePicture)
//        {
//            if (!string.IsNullOrWhiteSpace(profilePicture))
//            {
//                // Update the user's profile picture in the database
//                var user = _context.Users.FirstOrDefault(); // Replace with your own logic to retrieve the user
//                user.ProfilePicture = profilePicture;
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToAction("Index");
//        }
//    }
//}