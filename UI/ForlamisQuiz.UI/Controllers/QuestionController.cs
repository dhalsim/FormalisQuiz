using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormalisQuiz.Models;
using FormalisQuiz.DataLayer;

namespace ForlamisQuiz.UI.Controllers
{
    public class QuestionController : Controller
    {
        private FormalisQuizContext db = new FormalisQuizContext();

        //
        // GET: /Question/

        public ActionResult Index(int quizID)
        {
            return View(db.Questions.Where(question => question.Quiz.Id == quizID).ToList());
        }

        //
        // GET: /Question/Details/5

        public ActionResult Details(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // GET: /Question/Create

        public ActionResult Create(int quizId)
        {
            var questionOperations = new QuestionOperations();
            var question = questionOperations.Get(quizId) ?? new Question();
            return View(question);
        }

        //
        // POST: /Question/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question, int quizId)
        {
            if (ModelState.IsValid)
            {
                if (question.Id == 0)
                {
                    question.Quiz = db.Quizzes.Find(quizId);
                    db.Questions.Add(question);
                }
                else
                {
                    db.Questions.Attach(question);
                    db.Entry(question).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        //
        // GET: /Question/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // POST: /Question/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        //
        // GET: /Question/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // POST: /Question/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            question.Answers.Clear();


            db.Questions.Remove(question);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}