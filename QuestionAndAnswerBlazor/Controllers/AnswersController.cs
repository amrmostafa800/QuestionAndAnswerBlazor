using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAndAnswer.DTOs;
using QuestionAndAnswer.Services;
using System.Security.Claims;

namespace QuestionAndAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : Controller
    {
        IAnswerService _AnswerService;

        public AnswersController(IAnswerService answerService)
        {
            _AnswerService = answerService;
        }

        private bool _isValidUserAction(int AnswerID,int UserID)
        {
            var UserIdOfAnswer = _AnswerService.GetUserId(AnswerID);
            if (UserIdOfAnswer == UserID)
            {
                return true;
            }
            return false;
        }

        private int _GetUserID()
        {
            return int.Parse(User.FindFirstValue("ID")!);
        }

        [HttpPost("AddAnswer"), Authorize]
        public IActionResult AddAnswer(AddAnswerDTO addAnswer)
        {
            var Result = _AnswerService.AddNew(addAnswer.Answer, addAnswer.QuestionID, _GetUserID());
            return Ok();
        }

        [HttpPost("DeleteAnswer"), Authorize]
        public IActionResult DeleteAnswer([FromBody] int AnswerID)
        {
            if (_isValidUserAction(AnswerID, _GetUserID()))
            {
                var Result = _AnswerService.Remove(_GetUserID());
                return Ok(Result);
            }
            return BadRequest("Not Allowed");
        }

        [HttpPost("VoteToAnswer"), Authorize]
        public IActionResult VoteToAnswer(VoteDTO vote)
        {
            if (_isValidUserAction(vote.AnswerID, _GetUserID()))
            {
                var Answer = _AnswerService.TryVote(vote.AnswerID, vote.isUpVote, _GetUserID());
                return Ok(Answer);
            }
            return BadRequest("Not Allowed");
        }

        //maybe Better bycome not EditAble
        //[HttpPost("EditAnswer"), Authorize]
        //public IActionResult EditAnswer(EditAnswerDTO editAnswer)
        //{
        //    var ID = int.Parse(User.FindFirstValue("ID")!);
        //    var UserIdOfAnswer = _AnswerService.GetUserId(editAnswer.AnswerID);
        //    if (UserIdOfAnswer == ID) // Check If who Try Edit Is Same One Who Create Answer
        //    {
        //        var Result = _AnswerService.Edit(editAnswer.AnswerID, editAnswer.AnswerID);
        //        return Ok(Result);
        //    }
        //    return BadRequest("Not Allowed");
        //}
    }
}
