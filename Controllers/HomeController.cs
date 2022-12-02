using AddressGenerator.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressGenerator.Controllers
{   
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(string region = "Croatia", int seed = 25, double numberErrors = 5)
        {
            var people = new List<User>();            
            for (int i = seed; i <= seed + 250; i++)
            {                
                User user = new User(i, region, numberErrors);                
                people.Add(user.GenerateUser(user));
            }
            return View(people);
        }
    }
}