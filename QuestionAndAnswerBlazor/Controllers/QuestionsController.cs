﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAndAnswer.DTOs;
using QuestionAndAnswer.Services;
using System.Security.Claims;

namespace QuestionAndAnswer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : Controller
    {
        IQuestionsService _QuestionsService;
        
        public QuestionsController(IQuestionsService questionsService)
        {
            _QuestionsService = questionsService;
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
            var UserID = int.Parse(User.FindFirstValue("ID")!);
            var UserIdOfQuestion = _QuestionsService.GetUserId(QuestionID);
            if (UserIdOfQuestion == UserID)
            {
                var Result = _QuestionsService.Remove(UserID);
                return Ok(Result);
            }
            return BadRequest("Not Allowed");
        }

        [HttpPost("EditQuestion"), Authorize]
        public IActionResult EditQuestion(EditQuestionDTO editQuestion)
        {
            var UserID = int.Parse(User.FindFirstValue("ID")!);
            var UserIdOfQuestion = _QuestionsService.GetUserId(editQuestion.QuestionID);
            if (UserIdOfQuestion == UserID) // Check If who Try Edit Is Same One Who Create Question
            {
                var Result = _QuestionsService.Edit(editQuestion.QuestionID, editQuestion.NewQuestion);
                return Ok(Result);
            }
            return BadRequest("Not Allowed");
        }
    }
}