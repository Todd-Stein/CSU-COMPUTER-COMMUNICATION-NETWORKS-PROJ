using IssueTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers
{
    public class IssueController : Controller
    {
        private static List<IssueModel> _issues;
        
        private int _id = 0;
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IssueEntry(IssueModel newIssue)
        {
            if (ModelState.IsValid)
            {
                //newIssue.Id = _id++;
                _issues.Add(newIssue);

                return RedirectToAction("Success");
            }
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
