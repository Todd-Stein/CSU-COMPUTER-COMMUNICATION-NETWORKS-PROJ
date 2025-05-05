using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers
{
    public class PersonalAccessTokenController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangeToken(string newToken)
        {
            TempStorage.PersonalAccessToken = newToken;
            return RedirectToAction("Index");
        }
    }
}
