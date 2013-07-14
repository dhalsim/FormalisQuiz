using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using FormalisQuiz.Models;
using System.Data.Entity;

namespace FormalisQuiz.DataLayer.Repositories
{
    public class QuizRepository : RepositoryBase<FormalisQuizContext>
    {
        private readonly QuestionRepository _questionRepository = new QuestionRepository();

        public Quiz Find(int id)
        {
            using (DataContext)
            {
                return DataContext.Quizzes.Find(id);
            }
        }

        public Quiz GetWithQuestionsIncluded(int quizId)
        {
            using (DataContext)
            {
                return DataContext.Quizzes
                          .Include(q => q.Questions)
                          .FirstOrDefault(quiz1 => quiz1.Id == quizId);
            }
        }

        public Quiz GetWithQuestionsAndAnswersIncluded(int quizId)
        {
            using (DataContext)
            {
                return DataContext.Quizzes
                          .Include(q => q.Questions.Select(qu => qu.Answers))
                          .Single(quiz1 => quiz1.Id == quizId);
            }
        }

        public IEnumerable<Quiz> GetQuizzes()
        {
            return DataContext.Quizzes;
        }

        public void Create(Quiz quiz)
        {
            using (DataContext)
            {
                DataContext.Quizzes.Add(quiz);
                DataContext.SaveChanges();
            }
        }

        public void Update(Quiz quiz)
        {
            using (DataContext)
            {
                DataContext.Entry(quiz).State = EntityState.Modified;
                DataContext.SaveChanges();
            }
        }

        public void Delete(int quizId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (DeleteQuestionsAndAnswers(quizId)) return;
                DeleteQuiz(quizId);    
                scope.Complete();
            }
        }

        private void DeleteQuiz(int quizId)
        {
            using (DataContext)
            {
                Quiz quiz = DataContext.Quizzes.FirstOrDefault(q => q.Id == quizId);

                if (quiz != null)
                {
                    DataContext.Quizzes.Attach(quiz);
                    DataContext.Quizzes.Remove(quiz);
                    DataContext.SaveChanges();
                }
            }
        }

        private bool DeleteQuestionsAndAnswers(int quizId)
        {
            using (DataContext)
            {
                Quiz quiz = DataContext.Quizzes.Include("Questions").FirstOrDefault(q => q.Id == quizId);

                if (quiz != null)
                {
                    foreach (Question question in quiz.Questions)
                    {
                        _questionRepository.Delete(question.Id);
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}