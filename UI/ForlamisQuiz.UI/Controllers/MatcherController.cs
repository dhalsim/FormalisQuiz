using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ForlamisQuiz.UI.Filters;
using ForlamisQuiz.UI.Models;
using FormalisQuiz.DataLayer.Repositories;

namespace ForlamisQuiz.UI.Controllers
{
    [AdminAuthorization("Login", "Account")]
    public class MatcherController : BaseController
    {
        private readonly QuizUserRepository _quizUserRepository = new QuizUserRepository();
        private readonly QuizRepository _quizRepository = new QuizRepository();
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly QuestionRepository _questionRepository = new QuestionRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Delete(int userId, int quizId)
        {
            var quizUser = _quizUserRepository.Get(userId, quizId);
            _quizUserRepository.Delete(quizUser);

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var match = new UserQuizMatch
                            {
                                Users = _userRepository.GetUsers().ToList(),
                                Quizzes = _quizRepository.GetQuizzes().ToList()
                            };

            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int userId, int quizId)
        {
            if (ModelState.IsValid && userId != 0 && quizId != 0)
            {
                var user = _userRepository.Find(userId);
                var quiz = _quizRepository.Find(quizId);

                if (user != null && quiz != null)
                {
                    var quizUser = _quizUserRepository.Get(userId, quizId);

                    if (quizUser != null)
                    {
                        ViewBag.ErrorMessage = string.Format("Bu kullanıcı ({0} {1} - {2}) zaten bu sınava({3}) kayıtlıdır",
                            user.Name, user.Surname, user.UserName, quiz.Name);

                        return Create();
                    }

                    _quizUserRepository.Create(quiz, user);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Get(jQueryDataTableParamModel param)
        {
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<UserQuizMatch, string> orderingFunction =
                (c => sortColumnIndex == 1
                          ? c.UserName
                          : sortColumnIndex == 2
                                ? c.Name
                                : sortColumnIndex == 3
                                    ? c.Surname
                                    : c.QuizName
                                    );

            string filterText = param.sSearch == null ? string.Empty : param.sSearch.ToLower();

            var data = UserQuizMatches();

            var data2 = data
                .Where(d => d.Name.ToLower().Contains(filterText)
                            || d.Surname.ToLower().Contains(filterText)
                            || d.UserName.ToLower().Contains(filterText)
                            || d.QuizName.ToLower().Contains(filterText));


            string sortDirection = Request["sSortDir_0"]; // asc or desc
            IOrderedEnumerable<UserQuizMatch> data3 = sortDirection == "asc"
                                                         ? data2.OrderBy(orderingFunction)
                                                         : data2.OrderByDescending(orderingFunction);

            IEnumerable<string[]> dataTablesData = data3
                .Skip(param.iDisplayStart)
                .Take(param.iDisplayLength)
                .Select(s => new[]
                                 {
                                     s.UserId.ToString(),
                                     s.QuizId.ToString(),
                                     s.Name,
                                     s.Surname,
                                     s.UserName,
                                     s.QuizName,
                                     string.Format("{0}/{1}", s.CorrectAnswers, s.QuestionCount),
                                     s.Date.ToString(),
                                     null
                                 });

            var jsonData = new
            {
                sEcho = param.sEcho,
                iTotalRecords = data.Count(),
                iTotalDisplayRecords = 10,
                aaData = dataTablesData
            };

            return Json(jsonData);
        }

        private List<UserQuizMatch> UserQuizMatches()
        {
            var quizUsers = _quizUserRepository.GetAllWithUserAndQuizIncluded();
            var data = quizUsers
                          .Select(qu => new UserQuizMatch
                                            {
                                                UserId = qu.User.Id,
                                                QuizId = qu.Quiz.Id,
                                                Name = qu.User.Name,
                                                Surname = qu.User.Surname,
                                                UserName = qu.User.UserName,
                                                QuizName = qu.Quiz.Name,
                                                CorrectAnswers = qu.CorrectAnswers,
                                                Date = qu.Date,
                                                QuestionCount = _questionRepository.GetQuestionCount(qu.Quiz.Id)
                                            }).ToList();

            return data;
        }
    }
}