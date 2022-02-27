using Microsoft.AspNetCore.Mvc;
using StackOverFlowApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackOverFlowApi.Data.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace StackOverFlowApi.Controllers.Apis
{

    [ApiController]
    [Route("userQuestions")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserQuestionsApi : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly UnitOfWork unitOfWork;

        public UserQuestionsApi( UserManager<User> userManager, UnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IEnumerable<UserQuestion> getAll()
        {
           return this.unitOfWork.UserQuestionsManager.GetAll();
            //return manager.GetAll(); 
        } 
        [HttpPost]
        public async Task<string> add(string userName, int questionId)
        {
            var checkUser = this.unitOfWork.UserQuestionsManager.checkUserIsExist(userName);
            var checkQuestion = await this.unitOfWork.QuestionManager.Find(questionId);
            if (checkUser && checkQuestion != null) {
                var user = await userManager.FindByNameAsync(userName);
                var userQuestion = new UserQuestion
                {
                    UserId =user.Id, 
                    QuestionId = questionId, 
                    Type = 0
                };
                var add = this.unitOfWork.UserQuestionsManager.Insert(userQuestion);
                if (add == 200)
                {
                    return "Successed";
                }
            }
            return "Faild";
        }

        [HttpPut]
        public async Task<UserQuestion> update(string userId, int questionId, byte type)
        {
            var find = await this.unitOfWork.UserQuestionsManager.Find(userId, questionId);
            if (find != null)
            {
                var userQuestion = new UserQuestion
                {
                    UserId = userId, 
                    QuestionId = questionId, 
                    Type = type
                };
                var updated = this.unitOfWork.UserQuestionsManager.Update(userQuestion);
                if (updated != null)
                {
                    return await updated;
                }
            }
            return new UserQuestion { QuestionId = 0, UserId = "" };
        }

        [HttpDelete]
        public async Task<string> delete(string userId, int questionId)
        {
            var deleted = await this.unitOfWork.UserQuestionsManager.Delete(userId, questionId);
            if (deleted)
            {
                return "Successed";
            }
            else
            {
                return "Faild";
            }
        }

    }
}
