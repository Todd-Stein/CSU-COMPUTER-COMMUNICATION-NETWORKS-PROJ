using IssueTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers
{
    public class IssueController : Controller
    {   
        private static int _id = 0;
        [HttpGet]
        public IActionResult Index()
        {
            var issueList = TempStorage.IssueStorage;
            Console.WriteLine("Get request");
            return View(issueList);
        }
        [HttpPost]
        public ActionResult IssueEntry(IssueModel issue)
        {
            if (ModelState.IsValid)
            {
                issue.Id = _id++;
                if (issue.UserIds.Count()<=0)
                {
                    issue.Status = IssueStatus.ToBeAssigned;
                }
                else
                {
                    issue.Status = IssueStatus.CurrentlyWorkedOn;
                }
                TempStorage.IssueStorage.Add(issue.Id, issue);
                return RedirectToAction("Index");
            }
            return View(TempStorage.IssueStorage);
        }
        /*public IActionResult Success()
        {
            return View();
        }*/
    }
}
