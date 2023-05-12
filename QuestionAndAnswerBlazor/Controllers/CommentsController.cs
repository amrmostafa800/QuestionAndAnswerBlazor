using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAndAnswerBlazor.DTOs;
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

        [HttpPost("AddComment"), Authorize]
        public IActionResult AddComment(AddCommentDTO addComment)
        {
            if (addComment.isCommentOnAnswer)
            {
                var Result = _CommentsService.AddNew(addComment.Comment, addComment.ParentOrAnswerID, _GetUserID());
                return Ok(Result);
            }
            else if (addComment.isCommentOnAnswer == false)
            {
                var Result = _CommentsService.AddNew(addComment.Comment, addComment.ParentOrAnswerID, _GetUserID(), false);
                return Ok(Result);
            }
            return NotFound("Not Allowed");
        }
    }
}
