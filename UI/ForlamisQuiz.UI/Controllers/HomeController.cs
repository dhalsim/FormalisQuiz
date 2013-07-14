using System.Linq;
using System.Web.Mvc;
using ForlamisQuiz.UI.Filters;
using ForlamisQuiz.UI.Models;
using FormalisQuiz.Models;
using FormalisQuiz.DataLayer.Repositories;

namespace ForlamisQuiz.UI.Controllers
{
    [UserAuthorization("Login", "Account")]
    public class HomeController : BaseController
    {
        private readonly QuizUserRepository _quizUserRepository = new QuizUserRepository();
        private readonly QuizRepository _quizRepository = new QuizRepository();

        public ActionResult Index()
        {
            var user = SessionManager.UserContext.User;
            var quizUsers = _quizUserRepository.GetWithQuizzesAndQuestionsIncluded(user.Id);

            return View(new IndexModel { QuizUsers = quizUsers });
        }

        public ActionResult Take(int quizId)
        {
            var quiz = _quizRepository.GetWithQuestionsAndAnswersIncluded(quizId);

            foreach (Question question in quiz.Questions)
            {
                foreach (Answer answer in question.Answers)
                {
                    answer.IsCorrect = false;
                }
            }

            return View(quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Take(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                UpdateQuizUser(quiz);
            }

            return RedirectToAction("Index", "Home");
        }

        private void UpdateQuizUser(Quiz quiz)
        {
            int correctQuestionsCount = 0;

            var correctQuiz = _quizRepository.GetWithQuestionsAndAnswersIncluded(quiz.Id);

            var correctQuestions = correctQuiz.Questions.ToList();
            var userQuestions = quiz.Questions.ToList();

            for (int i = 0; i < userQuestions.Count; i++)
            {
                var correctQuestion = correctQuestions[i];
                var correctAnswers = correctQuestion.Answers.ToList();

                var userQuestion = userQuestions[i];
                var userAnswers = userQuestion.Answers.ToList();

                bool isCorrect = true;
                for (int j = 0; j < userAnswers.Count; j++)
                {
                    if (userAnswers[j].IsCorrect != correctAnswers[j].IsCorrect)
                    {
                        isCorrect = false;
                        break;
                    }
                }

                if (isCorrect)
                {
                    correctQuestionsCount++;
                }
            }

            var user = SessionManager.UserContext.User;
            var quizUser = _quizUserRepository.Get(user.Id, quiz.Id);

            _quizUserRepository.Update(quizUser, correctQuestionsCount);
        }
    }
}