using System.Web.Mvc;
namespace ForlamisQuiz.UI.Controllers
{
    public class HomeController : BaseController
    {
         public ActionResult Index()
         {
             return View();
         }
    }
}