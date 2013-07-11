using System.Data;
using System.Linq;
using System.Web.Mvc;
using FormalisQuiz.Models;
using FormalisQuiz.DataLayer;

namespace ForlamisQuiz.UI.Controllers
{
    public class QuizController : Controller
    {
        private readonly FormalisQuizContext _db = new FormalisQuizContext();

        //
        // GET: /Quiz/

        public ActionResult Index()
        {
            return View(_db.Quizzes.ToList());
        }

        //
        // GET: /Quiz/Details/5

        public ActionResult Details(int id = 0)
        {
            Quiz quiz = _db.Quizzes.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        //
        // GET: /Quiz/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Quiz/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                _db.Quizzes.Add(quiz);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quiz);
        }

        //
        // GET: /Quiz/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Quiz quiz = _db.Quizzes
                .Include("Questions")
                .FirstOrDefault(q => q.Id == id);
            
            if (quiz == null)
            {
                return HttpNotFound();
            }
            
            return View(quiz);
        }

        //
        // POST: /Quiz/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(quiz).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quiz);
        }

        //
        // GET: /Quiz/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Quiz quiz = _db.Quizzes.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        //
        // POST: /Quiz/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = _db.Quizzes.Find(id);
            _db.Quizzes.Remove(quiz);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}