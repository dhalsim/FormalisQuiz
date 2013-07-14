using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using FormalisQuiz.Models;

namespace FormalisQuiz.DataLayer.Repositories
{
    public class QuizUserRepository : RepositoryBase<FormalisQuizContext>
    {
        public IEnumerable<QuizUser> GetWithQuizzesAndQuestionsIncluded(int userId)
        {
            return DataContext.QuizUsers
                              .Where(qu => qu.User.Id == userId)
                              .Include(qu => qu.Quiz)
                              .Include(q => q.Quiz.Questions);
        }

        public QuizUser Get(int userId, int quizId)
        {
            using (DataContext)
            {
                return DataContext.QuizUsers.FirstOrDefault(qu => qu.UserId == userId && qu.QuizId == quizId);
            }
        }

        public List<QuizUser> GetAllWithUserAndQuizIncluded()
        {
            using (DataContext)
            {
                return DataContext.QuizUsers
                    .Include("User").Include("Quiz").ToList();
            }
        }

        public void Create(Quiz quiz, User user)
        {
            using (DataContext)
            {
                DataContext.QuizUsers.Add(new QuizUser { QuizId = quiz.Id, UserId = user.Id});
                DataContext.SaveChanges();
            }
        }

        public void Update(QuizUser quizUser, int correctQuestionsCount)
        {
            using (DataContext)
            {
                quizUser.CorrectAnswers = correctQuestionsCount;
                quizUser.Date = DateTime.Now;

                DataContext.QuizUsers.Attach(quizUser);
                DataContext.Entry(quizUser).State = EntityState.Modified;
                DataContext.SaveChanges();
            }
        }

        public void Delete(QuizUser quizUser)
        {
            using (DataContext)
            {
                DataContext.QuizUsers.Attach(quizUser);
                DataContext.Entry(quizUser).State = EntityState.Deleted;
                DataContext.SaveChanges();
            }
        }
    }
}