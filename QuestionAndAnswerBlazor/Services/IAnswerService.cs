namespace QuestionAndAnswerBlazor.Services
{
    public interface IAnswerService
    {
        bool AddNew(string Answer, int QuestionId, int UserId);
        int GetUserId(int AnswerId);
        bool Remove(int AnswerID);
        bool TryVote(int AnswerId, bool isUpVote, int UserID);
    }
}