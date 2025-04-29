using IssueTracker.Data;
using IssueTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    public class IssueController : Controller
    {
        private readonly IssueContext _ctx;
        private static int _id = 0;

        public IssueController(IssueContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<(int, string)> projects = _ctx.Projects.ToList().Select(val => (val.Id, val.Name)).ToList();
            return View(projects);
        }
        [HttpPost]
        public async Task<ActionResult> IssueToDo(string whattodo, string projectId)
        {
            var proj = _ctx.Projects.Find(Int32.Parse(projectId));
            ViewBag.CurrentProj = Int32.Parse(projectId);
            ViewBag.ProjectName =_ctx.Projects.Find(ViewBag.CurrentProj).Name;
            ViewBag.ProjectUsers = proj.ProjectLeadIDs;
            ViewBag.ProjectUsers.AddRange(proj.ProjectMemberIDs);
            Console.WriteLine(proj.ProjectIssues.Count.ToString());
            List<IssueModel> issueList = _ctx.Issues
                .Where(e => proj.ProjectIssues.Contains(e.Id))
                .ToList();
            //Console.WriteLine(issueList[0].Name);
            if (proj != null)
            {
                switch (whattodo)
                {
                    case "create":
                        return View("CreateIssue", Int32.Parse(projectId));
                        break;
                    case "delete":
                        return View("DeleteIssue",issueList);
                        break;
                    case "edit":
                        return View("EditIssue", issueList);
                        break;
                    default:
                        return RedirectToAction("Index");
                        break;
                }
            }
            return RedirectToAction("Index");   
        }
        [HttpPost]
        public async Task<ActionResult> IssueCreate(IssueModel newIssue, string projId)
        {
            newIssue.Id = _id++;
            ViewBag.CurrentProj = Int32.Parse(projId);
            
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
                //ProjectModel proj = TempStorage.ProjectStorage[Int32.Parse(projId)];
                _ctx.Issues.Add(newIssue);
                await _ctx.SaveChangesAsync();
                var newList = _ctx.Projects.Find(Int32.Parse(projId));
                ViewBag.ProjectName = newList.Name;
                newList.ProjectIssues.Add(newIssue.Id);
                await _ctx.Projects
                        .Where(p => p.Id == Int32.Parse(projId))
                        .ExecuteUpdateAsync(p => p
                        .SetProperty(f => f.ProjectIssues, newList.ProjectIssues));
                        //.ExecuteUpdateAsync(p => p
                        //.SetProperty(f => f.Name, project.Name)
                        //.SetProperty(f => f.Description, project.Description));
                await _ctx.SaveChangesAsync();
                var proj = _ctx.Projects.Find(Int32.Parse(projId));
                List<IssueModel> issueList = await _ctx.Issues
                    .Where(e => proj.ProjectIssues.Contains(e.Id))
                    .ToListAsync();
                //proj.ProjectIssues.Add(newIssue.Id);
                //TempStorage.IssueStorage.Add(newIssue.Id, newIssue);
                ViewBag.CurrentProj = Int32.Parse(projId);
                return View("EditIssue", issueList);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> IssueToEdit(List<IssueModel> issuesList, string projId)
        {
            Console.WriteLine("here");
            //if (ModelState.IsValid)
            //{
                ViewBag.CurrentProj = Int32.Parse(projId);
                foreach (var i in issuesList)
                {
                   // foreach (var issue in issuesList)
                    //{
                        await _ctx.Issues
                            .Where(p => p.Id == i.Id)
                            .ExecuteUpdateAsync(p => p
                            .SetProperty(f => f.Name, i.Name)
                            .SetProperty(f => f.Description, i.Description));
                    //}
                    await _ctx.SaveChangesAsync();
                    Console.WriteLine(i.Name);
                }
                Console.WriteLine("valid");
            //}
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> IssueToDelete(string issueProjId)
        {
            if (ModelState.IsValid)
            {
                string projId = issueProjId.Split(',')[0];
                Console.WriteLine(projId);
                string issueId = issueProjId.Split(',')[1];
                
                ViewBag.CurrentProj = Int32.Parse(projId);
                //bool exist = _ctx.Issues.Any(i => i.Id == int.Parse(issueId));
                var issue = _ctx.Issues.Find(Int32.Parse(issueId));
                if (issue != null)
                {
                    _ctx.Issues.Remove(issue);
                    var newList = _ctx.Projects.Find(Int32.Parse(projId)).ProjectIssues;
                    newList.Remove(Int32.Parse(issueId));
                    await _ctx.Projects
                            .Where(p => p.Id == Int32.Parse(projId))
                            .ExecuteUpdateAsync(p => p
                            .SetProperty(f => f.ProjectIssues, newList));
                    await _ctx.SaveChangesAsync();
                }
                /*if(TempStorage.IssueStorage.ContainsKey(Int32.Parse(issueId)))
                {
                    TempStorage.IssueStorage.Remove(Int32.Parse(issueId));
                }*/

            }
            return RedirectToAction("Index");
        }
    }
}
