using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class CommentApi : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public CommentApi( UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IEnumerable<Comments> Get()
        {
            return this.unitOfWork.CommentManager.GetAll();
        }
        [HttpGet("GetById")]
        public async Task<Comments> GetById(int id)
        {
            var find = await this.unitOfWork.CommentManager.Find(id);
            if (find != null)
            {
                return find;
            }
            return null;
        } 
        [HttpGet("GetResponsesByUserId")]
        public IEnumerable<Responces> GetResponsesByUserId(string userId)
        {
            return this.unitOfWork.CommentManager.getResponces(userId);
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
            var add = this.unitOfWork.CommentManager.Insert(comment);
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
            var update = await this.unitOfWork.CommentManager.Update(comment);
            if (update != null)
            {
                return update;
            }
            return null;
        } 

        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            var comment = await this.unitOfWork.CommentManager.Delete(id);
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
            var answer = await this.unitOfWork.CommentManager.getAnswer(questionId);
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
