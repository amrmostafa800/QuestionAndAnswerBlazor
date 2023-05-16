using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAndAnswerBlazor.DTOs;
using QuestionAndAnswerBlazor.Models;
using QuestionAndAnswerBlazor.Services;
using System.Security.Claims;

namespace QuestionAndAnswerBlazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : Controller
    {
        QuestionsService _QuestionsService;
        AnswerService _AnswerService;

        public QuestionsController(QuestionsService questionsService, AnswerService answerService)
        {
            _QuestionsService = questionsService;
            _AnswerService = answerService;
        }

        private int _GetUserID()
        {
            return int.Parse(User.FindFirstValue("ID")!);
        }

        private bool _isValidUserAction(int QuestionID, int UserID)
        {
            var UserIdOfQuestion = _QuestionsService.GetUserId(QuestionID);
            if (UserIdOfQuestion == UserID)
            {
                return true;
            }
            return false;
        }

        [HttpPost("AddQuestion"), Authorize]
        public IActionResult AddQuestion([FromBody] string Question)
        {
            var UserID = int.Parse(User.FindFirstValue("ID")!);
            var Result = _QuestionsService.AddNew(Question, UserID);
            return Ok(Result);
        }

        [HttpPost("DeleteQuestion"), Authorize]
        public IActionResult DeleteQuestion([FromBody] int QuestionID)
        {
            //Remove Answers On This Question
            _AnswerService.RemoveManyByQuestionID(QuestionID);

            var UserID = _GetUserID();
            if (_isValidUserAction(QuestionID,UserID))
            {
                var Result = _QuestionsService.Remove(UserID);
                return Ok(Result);
            }
            return BadRequest("Not Allowed");
        }

        [HttpPost("EditQuestion"), Authorize]
        public IActionResult EditQuestion(EditQuestionDTO editQuestion)
        {
            var UserID = _GetUserID();
            if (_isValidUserAction(editQuestion.QuestionID, UserID)) // Check If who Try Edit Is Same One Who Create Question
            {
                var Result = _QuestionsService.Edit(editQuestion.QuestionID, editQuestion.NewQuestion);
                return Ok(Result);
            }
            return BadRequest("Not Allowed");
        }
    }
}
