using System.Linq;
using System.Web.Mvc;
using FormalisQuiz.Models;
using FormalisQuiz.DataLayer.Repositories;

namespace ForlamisQuiz.UI.Controllers
{
    public class QuizController : Controller
    {
        private readonly QuizRepository _quizRepository = new QuizRepository();

        public ActionResult Index()
        {
            return View(_quizRepository.GetQuizzes());
        }

        public ActionResult Details(int id = 0)
        {
            Quiz quiz = _quizRepository.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                _quizRepository.Create(quiz);
                return RedirectToAction("Index");
            }

            return View(quiz);
        }

        public ActionResult Edit(int id = 0)
        {
            Quiz quiz = _quizRepository.GetWithQuestionsIncluded(id);
            
            if (quiz == null)
            {
                return HttpNotFound();
            }
            
            return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                _quizRepository.Update(quiz);
                return RedirectToAction("Index");
            }
            return View(quiz);
        }

        public ActionResult Delete(int id = 0)
        {
            Quiz quiz = _quizRepository.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _quizRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}