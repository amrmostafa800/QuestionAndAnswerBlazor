﻿using QuestionAndAnswerBlazor.Models;

namespace QuestionAndAnswerBlazor.Services
{
    public class CommentsService
    {
        private readonly Models.AppContext _context;

        public CommentsService(Models.AppContext context)
        {
            _context = context;
        }

        public bool AddNew(string Comment, int AnswerOrParentID, int UserID, bool isCommentOnAnswer = true)
        {
            if (isCommentOnAnswer)
            {
                Comment comment = new()
                {
                    AnswerId = AnswerOrParentID,
                    Comment1 = Comment
                };
                _context.Comments.Add(comment);
            }
            else
            {
                Comment comment = new()
                {
                    ParentId = AnswerOrParentID,
                    Comment1 = Comment,
                    UserId = UserID
                };
                _context.Comments.Add(comment);
            }

            _context.SaveChanges();
            return true;
        }

        public bool Remove(int CommentId)
        {
            var Comment = _context.Comments.SingleOrDefault(C => C.Id == CommentId);

            if (Comment != null)
            {
                _context.Comments.Remove(Comment);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Edit(int CommentId, string NewComment)
        {
            var Comment = _context.Comments.SingleOrDefault(C => C.Id == CommentId);

            if (Comment != null)
            {
                Comment.Comment1 = NewComment;
                _context.Comments.Update(Comment);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
