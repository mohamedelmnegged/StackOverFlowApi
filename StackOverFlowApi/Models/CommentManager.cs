using Microsoft.AspNetCore.Mvc;
using StackOverFlowApi.Data;
using StackOverFlowApi.Data.Repo;
using StackOverFlowApi.Data.Tables;
using StackOverFlowApi.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Models
{
    public class CommentManager : IServicesRepo<Comments>
    {
        private readonly ApplicationDBContext context;

        public CommentManager(ApplicationDBContext context)
        {
            this.context = context;
        }
        public int Insert(Comments e)
        {
            try
            {
                if (e != null)
                {
                    var checkUser = this.context.Users.Any(a => a.Id == e.UserId);
                    var checkQuestion = this.context.Questions.Any(a => a.Id == e.QuestionId);
                    if (checkUser && checkQuestion)
                    {
                        var insert = this.context.Comments.Add(e);
                        if (insert != null)
                        {
                            this.context.SaveChanges();
                            return 200;
                        }
                        else
                        {
                            return 400;
                        }
                    }else
                    {
                        return 300; // user or question is not found 
                    }
                }
                else
                {
                    return 404;
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

        }
        public async Task< Comments> Find(int id)
        {
            if (id != null)
            {
                var comment = await this.context.Comments.FindAsync(id);
                if (comment != null)
                {
                    return comment;
                }
            }
            return null;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var find =await Find(id);
                if (find != null)
                {
                    var delete = this.context.Comments.Remove(find);
                    this.context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }



        public IEnumerable<Comments> GetAll()
        {
            return this.context.Comments.Where(i => i.Id > 0);
        }

        public async Task<Comments> Update(Comments e)
        {
            try
            {
                var find =  await Find(e.Id);
                if (find != null)
                {
                    find.Flag = e.Flag;
                    find.Body = e.Body;
                    find.DateTime = e.DateTime;
                    var update = this.context.Comments.Update(find);
                    if (update != null)
                    {
                        this.context.SaveChanges();
                        return find;
                    }
                }
                return null;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        public async Task<IEnumerable<Answers>> getAnswer(int questionId)
        {
           var answers =  context.Questions.Where(a => a.Id == questionId)
                .Join(
                context.Comments.Where(s => s.Flag == Enums.Flag.answer),
                question => question.Id, 
                comment => comment.QuestionId, 
                (question, comment) => new Answers{ 
                    AnswerBody = comment.Body, 
                    AnswerUpvoted = comment.Upvoted, 
                    AnswerDownvoted = comment.Downvoted,
                    UserId = comment.UserId
                }); 
            if(answers != null)
            {
                return answers; 
            }
            return null; 
        }
        public IEnumerable<Responces> getResponces(string userId)
        {
            var check = this.context.OwnUser.Any(a => a.Id == userId);
            if (check)
            {
                return this.context.OwnUser.Where(a => a.Id == userId)
                    .Join(this.context.Comments.Where(s => s.Flag == Enums.Flag.comment),
                    user => user.Id,
                    comment => comment.UserId,
                    (user, comment) => new Responces
                    {
                        CommentId = comment.Id,
                        Body = comment.Body,
                        Time = comment.DateTime,
                        QuestionId = comment.QuestionId,
                        UserId = user.Id
                    });
            }
            return null;
        }
    }
}
