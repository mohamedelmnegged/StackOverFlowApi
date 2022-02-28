using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using StackOverFlowApi.Data.Tables;
using Microsoft.AspNetCore.Identity;
using StackOverFlowApi.Models;
using StackOverFlowApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackOverFlowApi.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace StackOverFlowApi.Controllers.Apis
{

    [ApiController]
    [Route("questions")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class QuestionsApi : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly UnitOfWork unitOfWork;

        public QuestionsApi(UserManager<User> userManager, UnitOfWork unitOfWork )
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IEnumerable<Question> getAll()
        {
            return this.unitOfWork.QuestionManager.GetAll();
        } 
        [HttpGet("GetById")]
        public async Task<Question> GetQuestion(int id)
        {
            var find = await this.unitOfWork.QuestionManager.Find(id);
            if(find != null)
            {
                return find; 
            }
            return null; 
        }
        [HttpGet("GetQuestionByUserId")]
        public async Task<IEnumerable< Question>> GetQuestionByUserId(string userId)
        {
            var questions = await this.unitOfWork.QuestionManager.getByUserId(userId);
            if(questions != null)
            {
                return questions;
            }
            return null; 
        } 

        [HttpGet("GetHomeQuestions")]
        public IEnumerable<HomeQuestions> GetHomeQuestions()
        {
            return this.unitOfWork.QuestionManager.GetHomeQuestions();
        } 
        //[HttpGet("GetNewestHomeQuestions")]
        //public IEnumerable<HomeQuestions> GetNewestHomeQuestions()
        //{
        //    return this.unitOfWork.QuestionManager.NewestHomeQuestion();
        //}
        //[HttpGet("GetViewsHomeQuestions")]
        //public IEnumerable<HomeQuestions> GetViewstHomeQuestions()
        //{
        //    return this.unitOfWork.QuestionManager.ViewsHomeQuestion();
        //}
        [HttpPost]
        public async Task<string> addQuestion(string userName, string questionTitle, string questionBody )
        {
            var check = this.unitOfWork.QuestionManager.checkUserIsExist(userName);
            if (check)
            {
                var user = await userManager.FindByNameAsync(userName);
                var question = new Question
                {
                    Title = questionTitle,
                    Body = questionBody,
                    Enable = true,
                    User = user,
                    Answered = false,
                    Downvoted = 0,
                    Upvoted = 0,
                    Views = 0,
                    Status = Enums.status.valid
                };
                var status = this.unitOfWork.QuestionManager.Insert(question);
                if (status == 200)
                {
                    return "Success";
                }
            }
            return "Faild";
        }
        
        [HttpPut] 
        public async Task<Question> update(int id, string questionBody, string questionTitle)
        {
            var find = await this.unitOfWork.QuestionManager.Find(id);
            if (find != null)
            {
                var question = new Question
                {
                    Id = id,
                    Body  = questionBody, 
                    Title = questionTitle
                };
                var updated = this.unitOfWork.QuestionManager.Update(question);
                if (updated != null)
                {
                    return await updated;
                }
            }
            return find;
        }
        [HttpDelete]
        public async Task<string> delete(int id)
        {
            var deleted = await this.unitOfWork.QuestionManager.Delete(id);
            if (deleted)
            {
                return "Successed";
            }
            else
            {
                return "Faild";
            }
        }

        [HttpGet("OrderByViews")]
        public IEnumerable<Question> OrderByViews()
        {
            return this.unitOfWork.QuestionManager.OrderBy(a => a.Views, "ASC"); 
        }
        [HttpGet("GetQuestionsByTagsAndOrderedViews")]
        public IEnumerable<QuestionTagView> GetQuestionsByTagsAndOrderedViews(int tagId)
        {
            return this.unitOfWork.QuestionManager.GeQuestionsByTagIdOrdered(tagId);
        }

    }
}
