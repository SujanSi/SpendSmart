using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;
using System.Diagnostics;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SpendSmartDbContext _context;


        public HomeController(ILogger<HomeController> logger , SpendSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
/// <summary>
/// </summary>
/// <returns></returns>
        public IActionResult Expenses()
        {
            var allExpenses = _context.Expenses.ToList();
            return View(allExpenses);
        }

        public IActionResult CreateNewOrEdit(int? id)
        {
            if (id != null)
            {
                var expenseInDb = _context.Expenses.FirstOrDefault(x => x.Id == id);
                return View(expenseInDb);
            }
            return View();
        }

        public IActionResult CreateNewForm(Expense model)
        {
            if(model.Id==0)
            {
                _context.Expenses.Add(model);

            }
            else
            {
                _context.Expenses.Update(model);
            }
            
            _context.SaveChanges();

            return RedirectToAction("Expenses");
        }

        public IActionResult DeleteExpenses(int id)
        {
            var expenseInDb = _context.Expenses.FirstOrDefault(x => x.Id == id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
