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

namespace StackOverFlowApi.Controllers.Apis
{

    [ApiController]
    [Route("questions")]
    public class QuestionsApi : ControllerBase
    {
        private readonly QuestionManager questionManager;
        private readonly UserManager<User> userManager;
        public QuestionsApi(QuestionManager questionManager, UserManager<User> userManager )
        {
            this.questionManager = questionManager;
            this.userManager = userManager;

        }
        [HttpGet]
        public IEnumerable<Question> getAll()
        {
            return questionManager.GetAll();
        } 
        [HttpGet("GetById")]
        public async Task<Question> GetQuestion(int id)
        {
            var find = await questionManager.Find(id);
            if(find != null)
            {
                return find; 
            }
            return null; 
        }
        [HttpGet("GetQuestionByUserId")]
        public async Task<IEnumerable< Question>> GetQuestionByUserId(string userId)
        {
            var questions = await questionManager.getByUserId(userId);
            if(questions != null)
            {
                return questions;
            }
            return null; 
        } 

        [HttpGet("GetHomeQuestions")]
        public IEnumerable<HomeQuestions> GetHomeQuestions()
        {
            return this.questionManager.GetHomeQuestions();
        }
        [HttpPost]
        public async Task<string> addQuestion(string userName, string questionTitle, string questionBody )
        {
            var check =  questionManager.checkUserIsExist(userName);
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
                var status = this.questionManager.Insert(question);
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
            var find = await questionManager.Find(id);
            if (find != null)
            {
                var question = new Question
                {
                    Id = id,
                    Body  = questionBody, 
                    Title = questionTitle
                };
                var updated = questionManager.Update(question);
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
            var deleted = await questionManager.Delete(id);
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
            return questionManager.OrderBy(a => a.Views, "ASC"); 
        }
        [HttpGet("GetQuestionsByTagsAndOrderedViews")]
        public IEnumerable<QuestionTagView> GetQuestionsByTagsAndOrderedViews(int tagId)
        {
            return questionManager.GeQuestionsByTagIdOrdered(tagId);
        }

    }
}
