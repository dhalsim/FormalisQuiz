using System.Web.Mvc;
using FormalisQuiz.Models;
using ForlamisQuiz.UI.Filters;
using FormalisQuiz.DataLayer.Repositories;

namespace ForlamisQuiz.UI.Controllers
{
    [AdminAuthorization("Login", "Account")]
    public class QuestionController : Controller
    {
        private readonly QuestionRepository _questionRepository = new QuestionRepository();
        private readonly QuizRepository _quizRepository = new QuizRepository();

        public ActionResult Index(int quizId)
        {
            return View(_questionRepository.GetQuestions(quizId));
        }

        public ActionResult Create(int quizId)
        {
            var quiz = _quizRepository.Find(quizId);
            return View(new Question { Quiz = quiz });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question, int quizId)
        {
            if (ModelState.IsValid)
            {
                _questionRepository.Create(question, quizId);
                return RedirectToAction("Index", new { quizId = quizId });
            }

            return View(question);
        }

        public ActionResult Edit(int id = 0)
        {
            Question question = _questionRepository.GetWithQuizAndAnswersIncluded(id);

            if (question == null)
            {
                return HttpNotFound();
            }

            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                _questionRepository.Update(question);
                return RedirectToAction("Index", new { quizId = question.Quiz.Id });
            }
            return View(question);
        }

        public ActionResult Delete(int id = 0)
        {
            Question question = _questionRepository.GetWithQuizIncluded(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int quizId = _questionRepository.Delete(id);
            return RedirectToAction("Index", "Quiz", new { quizId = quizId });
        }
    }
}