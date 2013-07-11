using System.Collections.Generic;
using System.Linq;
using FormalisQuiz.Models;

namespace FormalisQuiz.DataLayer
{
    public class QuestionOperations
    {
        public Question Get(int i)
        {
            using (var dbContext = new FormalisQuizContext(false))
            {
                return dbContext.Questions
                    .Include("Answers")
                    .FirstOrDefault(q => q.Id == i);
            }
        }

        public List<Question> GetQuestions(int quizId)
        {
            using (var dbContext = new FormalisQuizContext(false))
            {
                return dbContext.Questions
                    .Where(question => question.Quiz.Id == quizId)
                    .ToList();
            }
        } 
    }
}