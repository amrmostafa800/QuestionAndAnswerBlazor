using QuestionAndAnswerBlazor.Models;

namespace QuestionAndAnswerBlazor.Services
{
    public class AnswerService
    {
        private readonly Models.AppContext _context;

        public AnswerService(Models.AppContext context)
        {
            _context = context;
        }

        public bool AddNew(string Answer, int QuestionId, int UserId)
        {
            Answer NewAnswer = new()
            {
                Answer1 = Answer,
                QuestionId = QuestionId,
                UserId = UserId
            };

            _context.Answers.Add(NewAnswer);
            _context.SaveChanges();

            return true;
        }

        public bool Remove(int AnswerID)
        {
            var Answer = _context.Answers.SingleOrDefault(A => A.Id == AnswerID);
            if (Answer != null)
            {
                _context.Answers.Remove(Answer);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetUserId(int AnswerId)
        {
            var Answer = _context.Answers.SingleOrDefault(A => A.Id == AnswerId);

            if (Answer != null)
            {
                return Answer.UserId;
            }
            return 0; // Invalid AnswerId  
        }

        private void _ReverseVoteType(int AnswerId, bool isUpVote, int UserID)
        {
            TryVote(AnswerId, !isUpVote, UserID);
            TryVote(AnswerId, isUpVote, UserID);
        }

        private bool _Vote(int AnswerId, bool isUpVote)
        {
            var Answer = _context.Answers.SingleOrDefault(A => A.Id == AnswerId);
            if (Answer != null)
            {
                if (isUpVote)
                {
                    Answer.UpVotes += 1;
                }
                else
                {
                    Answer.DownVotes += 1;
                }
                _context.Answers.Update(Answer);

                Vote vote = new()
                {
                    AnswerId = AnswerId,
                    IsUpVote = isUpVote,
                    UserId = Answer.UserId
                };

                _context.Votes.Add(vote);

                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private bool _UnVote(int AnswerId, bool isUpVote)
        {
            var Answer = _context.Answers.SingleOrDefault(A => A.Id == AnswerId);
            if (Answer != null)
            {
                if (isUpVote)
                {
                    Answer.UpVotes -= 1;
                }
                else
                {
                    Answer.DownVotes -= 1;
                }
                _context.Answers.Update(Answer);

                Vote vote = _context.Votes.SingleOrDefault(V => V.UserId == Answer.UserId && V.AnswerId == AnswerId)!;

                if (vote != null)
                {
                    _context.Votes.Remove(vote);

                    _context.SaveChanges();
                    return true;
                }
                return false;

            }
            return false;
        }

        public bool TryVote(int AnswerId, bool isUpVote, int UserID)
        {
            var Vote = _context.Votes.SingleOrDefault(V => V.UserId == UserID && V.AnswerId == AnswerId);
            if (Vote == null)
            {
                _Vote(AnswerId, isUpVote);
                return true;
            }
            else if (Vote != null && Vote.IsUpVote == isUpVote)
            {
                _UnVote(AnswerId, isUpVote);
                return true;
            }
            else if (Vote != null && Vote.IsUpVote != isUpVote)
            {
                // Switch From UpVote To DownVote or Reverse
                _ReverseVoteType(AnswerId, isUpVote, UserID);
                return true;
            }

            return false;
        }
    }
}
