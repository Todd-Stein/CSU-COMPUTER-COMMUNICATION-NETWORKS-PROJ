using IssueTracker.Data;
using IssueTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    public class ProjectController : Controller
    {
        private static int _id = 0;

        private readonly IssueContext _ctx;
        public ProjectController(IssueContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProjectToDo(string whattodo)
        {
            var projList = _ctx.Projects.ToList();
            switch (whattodo)
            {
                case "create":
                    return View("CreateProject");
                    break;
                case "delete":
                    return View("DeleteProject", projList);
                    break;
                case "edit":
                    return View("EditProject", projList);
                    break;
                default:
                    return RedirectToAction("Index");
                    break;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> ProjectCreate(ProjectModel newProject)
        {
            newProject.Id = _id++;
            if (newProject.ProjectLeadIDs.Count < 0)
            {
                return View("CreateProject");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var projList = _ctx.Projects.ToList();
                    //TempStorage.ProjectStorage.Add(newProject.Id, newProject);
                    _ctx.Projects.Add(newProject);
                    await _ctx.SaveChangesAsync();
                    return View("EditProject", projList);
                }
                else
                {
                    Console.WriteLine("invalid");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public async Task<ActionResult> ProjectToEdit(List<ProjectModel> projectsList)
        {
            Console.WriteLine("wfnwjf");
            //if (ModelState.IsValid)
            //{
                foreach (var project in projectsList)
                {
                    Console.WriteLine("project Id\t"+project.Id.ToString());
                    Console.WriteLine("project name\t" + project.Name);
                await _ctx.Projects
                        .Where(p => p.Id == project.Id)
                        .ExecuteUpdateAsync(p => p
                        .SetProperty(f => f.Name, project.Name)
                        .SetProperty(f => f.Description, project.Description));
                    //TempStorage.ProjectStorage[project.Id].Name = project.Name;
                    //TempStorage.ProjectStorage[project.Id].Description = project.Description;
                }
                Console.WriteLine("Updated fields");
                await _ctx.SaveChangesAsync();
            //}
            //else
            //{
            /*    Console.WriteLine("invalid");
                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine("Key:\t" + key.ToString());
                        Console.WriteLine("Error:\t" + error.ToString());
                    }
                }*/
            //}
            return View("Index");
        }
        [HttpPost]
        public async Task<ActionResult> ProjectToDelete(string projectId)
        {
            //if (ModelState.IsValid)
            //{
                Console.WriteLine($"Project {projectId}");
                var project = _ctx.Projects.Find(Int32.Parse(projectId));
                if (project != null)
                {
                    _ctx.Projects.Remove(project);

                    await _ctx.SaveChangesAsync();
                    Console.WriteLine(project.Id.ToString());
                    //TempStorage.ProjectStorage.Remove(Int32.Parse(projectId));
                }
                else
                {
                    Console.WriteLine("Not found");
                }
                
            //}
            return View("Index");
        }
    }
}
