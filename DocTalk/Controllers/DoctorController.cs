using Microsoft.AspNetCore.Mvc;

namespace DocTalk.Web.Controllers
{
    // The [Route] attribute sets the base URL for this controller.
    // In this case, it will be something like "/dashboard".
    public class DashboardController : Controller
    {
        // This is the default action. When the user navigates to "/dashboard",
        // this method will be called and it will return the main dashboard view.
        public IActionResult Index()
        {
            // The Index view is the main dashboard you've been working on.
            return View();
        }

        // This action handles the "Patients" page. It will be accessible at "/dashboard/patients".
        public IActionResult Patients()
        {
            // You can add logic here to fetch patient data from a database
            // and pass it to the view.
            return View();
        }

        // This action handles the "Reports" page. It will be accessible at "/dashboard/reports".
        public IActionResult Reports()
        {
            // You can add logic here to generate or retrieve reports.
            return View();
        }

        // This action handles the "Settings" page. It will be accessible at "/dashboard/settings".
        public IActionResult Settings()
        {
            return View();
        }
    }
}
