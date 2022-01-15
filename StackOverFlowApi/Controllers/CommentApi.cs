using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StackOverFlowApi.Data.Tables;
using StackOverFlowApi.Models;
using StackOverFlowApi.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Controllers
{
    [ApiController]
    [Route("Comments")]
    public class CommentApi : Controller
    {
        private readonly CommentManager commentManager;

        public CommentApi(CommentManager commentManager)
        {
            this.commentManager = commentManager;
        }
        [HttpGet]
        public IEnumerable<Comments> Get()
        {
            return commentManager.GetAll();
        }
        [HttpGet("GetById")]
        public async Task<Comments> GetById(int id)
        {
            var find = await commentManager.Find(id);
            if (find != null)
            {
                return find;
            }
            return null;
        } 
        [HttpGet("GetResponsesByUserId")]
        public IEnumerable<Responces> GetResponsesByUserId(string userId)
        {
            return commentManager.getResponces(userId);
        }
        [HttpPost]
        public string Add(string body, string userId, int questionId)
        {
            var comment = new Comments
            {
                Body = body,
                DateTime = DateTime.Now,
                QuestionId = questionId,
                UserId = userId,
                Upvoted = 0,
                Flag = Enums.Flag.comment
            };
            var add = commentManager.Insert(comment);
            if (add == 200)
            {
                return "Added Successfuly";
            }
            else
            {
                return "Faild";
            }
        }
        [HttpPut]
        public async Task<Comments> Update(int id, string body, int flag)
        {
            var comment = new Comments
            {
                Id = id,
                DateTime = DateTime.Now,
                Body = body,
                Flag = (int)Enums.Flag.comment == flag ? Enums.Flag.comment : Enums.Flag.answer
            };
            var update = await commentManager.Update(comment);
            if (update != null)
            {
                return update;
            }
            return null;
        } 

        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            var comment = await commentManager.Delete(id);
            if (comment)
            {
                return "Successfuly";
            }
            else
            {
                return "Faild";
            }
        }

        [HttpGet("GetQuestionAnswer")]
        public async Task<IEnumerable<Answers>> GetQuestionAnswer(int questionId)
        {
            var answer = await commentManager.getAnswer(questionId);
            if(answer != null)
            {
                return answer;
            }
            else
            {
                return null;
            }
           
        }
    }
}
