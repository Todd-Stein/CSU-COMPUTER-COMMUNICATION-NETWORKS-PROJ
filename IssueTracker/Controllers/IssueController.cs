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
            //var issueList = TempStorage.IssueStorage;
            //Console.WriteLine("Get request");
            return View();
        }
        [HttpPost]
        public ActionResult IssueToDo(string whattodo, string projectId)
        {
            Console.WriteLine("action:\t"+whattodo);
            Console.WriteLine("id:\t" + projectId);
            switch (whattodo)
            {
                case "create":
                    return View("CreateIssue", Int32.Parse(projectId));
                    break;
                case "delete":
                    View("DeleteIssue", projectId);
                    break;
                case "edit":
                    return View("EditIssue", Int32.Parse(projectId));
                    break;
                default:
                    RedirectToAction("Index");
                    break;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult IssueCreate(IssueModel newIssue, string projId)
        {
            newIssue.Id = _id++;
            if (newIssue.UserIds.Count > 0)
            {
                newIssue.Status = IssueStatus.CurrentlyWorkedOn;
            }
            else
            {
                newIssue.Status = IssueStatus.ToBeAssigned;
            }
            
            if (ModelState.IsValid)
            {
                Console.WriteLine("Valid");
                ProjectModel proj = TempStorage.ProjectStorage[Int32.Parse(projId)];

                proj.ProjectIssues.Add(newIssue.Id);
                TempStorage.IssueStorage.Add(newIssue.Id, newIssue);
                Console.WriteLine("Make issue");
                foreach (var p in proj.ProjectIssues)
                {
                    Console.WriteLine(p);
                }
                return View("EditIssue", ViewBag.CurrentProj);
            }
            Console.WriteLine("invalid");
            return View("Index");
        }
        /*public IActionResult Success()
        {
            return View();
        }*/
    }
}
