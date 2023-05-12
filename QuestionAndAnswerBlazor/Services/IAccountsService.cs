namespace QuestionAndAnswer.Services
{
    public interface IAccountsService
    {
        bool AddNew(string User, string Password, string Email);
        int LoginIn(string Username, string Password);
    }
}