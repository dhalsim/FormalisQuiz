using System.Linq;
using System.Web.Mvc;
using FormalisQuiz.Models;
using ForlamisQuiz.UI.Filters;
using FormalisQuiz.DataLayer.Repositories;
using FormalisQuiz.Utilities;

namespace ForlamisQuiz.UI.Controllers
{
    [AdminAuthorization("Login", "Account")]
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository = new UserRepository();

        public ActionResult Index()
        {
            return View(_userRepository.GetUsers());
        }

        public ActionResult Details(int id = 0)
        {
            User user = _userRepository.Find(id);
            
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = CryptoHelper.ConvertToMd5Hash(user.Password);
                _userRepository.Create(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Edit(int id = 0)
        {
            User user = _userRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.Password = string.Empty;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Update(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Delete(int id = 0)
        {
            User user = _userRepository.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _userRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}