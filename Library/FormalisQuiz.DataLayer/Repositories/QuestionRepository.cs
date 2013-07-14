using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using FormalisQuiz.Models;

namespace FormalisQuiz.DataLayer.Repositories
{
    public class QuestionRepository : RepositoryBase<FormalisQuizContext>
    {
        public List<Question> GetQuestions(int quizId)
        {
            using (DataContext)
            {
                return DataContext.Questions
                   .Include("Quiz")
                   .Where(question => question.Quiz.Id == quizId)
                   .ToList();
            }
        }

        public Question GetWithQuizAndAnswersIncluded(int questionId)
        {
            using (DataContext)
            {
                return DataContext.Questions
                    .Include("Quiz")
                    .Include("Answers")
                    .FirstOrDefault(q => q.Id == questionId);
            }
        }

        public void Create(Question question, int quizId)
        {
            using (DataContext)
            {
                if (question.Id == 0)
                {
                    question.Quiz = DataContext.Quizzes.Find(quizId);
                    DataContext.Questions.Add(question);
                }
                else
                {
                    DataContext.Questions.Attach(question);
                    DataContext.Entry(question).State = EntityState.Modified;
                }

                DataContext.SaveChanges();
            }
        }

        public void Update(Question question)
        {
            using (DataContext)
            {
                DataContext.Entry(question).State = EntityState.Modified;

                foreach (var answer in question.Answers)
                {
                    DataContext.Entry(answer).State = EntityState.Modified;
                }

                DataContext.SaveChanges();
            }
        }

        public int Delete(int questionId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DeleteAnswers(questionId);
                int quizId = DeleteQuestion(questionId);
                scope.Complete();
                return quizId;
            }
        }

        private int DeleteQuestion(int questionId)
        {
            using (DataContext)
            {
                Question question = DataContext.Questions
                                               .Include("Answers")
                                               .Include("Quiz")
                                               .FirstOrDefault(q => q.Id == questionId);

                if (question != null)
                {
                    int quizId = question.Quiz.Id;

                    DataContext.Questions.Attach(question);
                    DataContext.Questions.Remove(question);
                    DataContext.SaveChanges();

                    return quizId;
                }

                return 0;
            }
        }

        private void DeleteAnswers(int questionId)
        {
            using (DataContext)
            {
                var answers = DataContext.Answers
                                         .Include("Question")
                                         .Where(a => a.Question.Id == questionId);

                foreach (Answer answer in answers)
                {
                    DataContext.Answers.Attach(answer);
                    DataContext.Answers.Remove(answer);
                }

                DataContext.SaveChanges();
            }
        }

        public Question GetWithQuizIncluded(int id)
        {
            using (DataContext)
            {
                return DataContext.Questions
                    .Include("Quiz")
                    .FirstOrDefault(q => q.Id == id);
            }
        }

        public int GetQuestionCount(int quizId)
        {
            using (DataContext)
            {
                return DataContext.Questions.Include("Quiz").Count(q => q.Quiz.Id == quizId);
            }
        }
    }
}