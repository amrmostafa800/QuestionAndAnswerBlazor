using QuestionAndAnswer.Models;

namespace QuestionAndAnswer.Services
{
    public class QuestionsService : IQuestionsService
    {
        private readonly Models.AppContext _context;

        public QuestionsService(Models.AppContext context)
        {
            _context = context;
        }

        public bool AddNew(string Question, int UserId)
        {
            Question NewQuestion = new()
            {
                Question1 = Question,
                UserId = UserId
            };
            _context.Questions.Add(NewQuestion);
            _context.SaveChanges();

            return true;
        }

        public bool Remove(int QuestionId)
        {
            var Question = _context.Questions.SingleOrDefault(Q => Q.Id == QuestionId);
            if (Question != null)
            {
                _context.Remove(Question);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Edit(int QuestionId, string NewQuestion)
        {
            var Question = _context.Questions.SingleOrDefault(Q => Q.Id == QuestionId);
            if (Question != null)
            {
                Question.Question1 = NewQuestion;
                _context.Questions.Update(Question);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetUserId(int QuestionId)
        {
            var Question = _context.Questions.SingleOrDefault(Q => Q.Id == QuestionId);

            if (Question != null)
            {
                return Question.UserId;
            }
            return 0; // Invalid QuestionId  
        }
    }
}
