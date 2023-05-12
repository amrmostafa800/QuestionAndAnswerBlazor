namespace QuestionAndAnswer.Services
{
    public interface IQuestionsService
    {
        bool AddNew(string Question, int UserId);
        bool Edit(int QuestionId, string NewQuestion);
        int GetUserId(int QuestionId);
        bool Remove(int QuestionId);
    }
}