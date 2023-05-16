using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAndAnswerBlazor.DTOs;
using QuestionAndAnswerBlazor.Models;
using QuestionAndAnswerBlazor.Services;
using System.Security.Claims;

namespace QuestionAndAnswerBlazor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        CommentsService _CommentsService;

        public CommentsController(CommentsService commentsService)
        {
            _CommentsService = commentsService;
        }

        private int _GetUserID()
        {
            return int.Parse(User.FindFirstValue("ID")!);
        }

        private bool _isValidUserAction(int AnswerID, int UserID)
        {
            var UserIdOfAnswer = _CommentsService.GetUserId(AnswerID);
            if (UserIdOfAnswer == UserID)
            {
                return true;
            }
            return false;
        }

        [HttpPost("AddComment"), Authorize]
        public IActionResult AddComment(AddCommentDTO addComment)
        {
            var ID = _GetUserID();
            if (addComment.isCommentOnAnswer)
            {
                var Result = _CommentsService.AddNew(addComment.Comment, addComment.ParentOrAnswerID, ID);
                return Ok(Result);
            }
            else if (addComment.isCommentOnAnswer == false)
            {
                var Result = _CommentsService.AddNew(addComment.Comment, addComment.ParentOrAnswerID, ID, false);
                return Ok(Result);
            }
            return NotFound("Not Allowed");
        }

        [HttpPost("DeleteComment"), Authorize]
        public IActionResult DeleteComment([FromBody] int CommentID)
        {
            if (_isValidUserAction(CommentID, _GetUserID()))
            {
                var Result = _CommentsService.Remove(CommentID);
                return Ok(Result);
            }
            return NotFound("Not Allowed");
        }
    }
}
