using IssueTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers
{
    public class ProjectController : Controller
    {
        private static int _id;
        [HttpGet]
        public IActionResult Index()
        {
            var projectList = TempStorage.ProjectStorage.Values.ToList();
            return View(projectList);
        }
        [HttpPost]
        public ActionResult ProjectEntry(ProjectModel proj)
        {
            if (ModelState.IsValid)
            {
                var project = TempStorage.ProjectStorage;
                proj.Id = _id++;
                /*if (proj.ProjectMemberIDs.Count > 0)
                {
                    Console.WriteLine("not empty");
                }
                else
                {
                    Console.WriteLine("empty");
                }*/
                    TempStorage.ProjectStorage.Add(proj.Id, proj);
                return RedirectToAction("Index");
            }
            return View(TempStorage.ProjectStorage.ToList());
        }
    }
}
