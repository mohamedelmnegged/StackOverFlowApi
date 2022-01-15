using Microsoft.AspNetCore.Mvc;
using StackOverFlowApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackOverFlowApi.Data.Tables;
using Microsoft.AspNetCore.Identity;

namespace StackOverFlowApi.Controllers.Apis
{

    [ApiController]
    [Route("userQuestions")]
    public class UserQuestionsApi : ControllerBase
    {
        private readonly UserQuestionsManager manager;
        private readonly UserManager<User> userManager;
        private readonly QuestionManager questionManager;

        public UserQuestionsApi(UserQuestionsManager manager, UserManager<User> userManager, QuestionManager questionManager)
        {
            this.manager = manager;
            this.userManager = userManager;
            this.questionManager = questionManager;
        }
        [HttpGet]
        public IEnumerable<UserQuestion> getAll()
        {
            return manager.GetAll(); 
        } 
        [HttpPost]
        public async Task<string> add(string userName, int questionId)
        {
            var checkUser = manager.checkUserIsExist(userName);
            var checkQuestion = await questionManager.Find(questionId);
            if (checkUser && checkQuestion != null) {
                var user = await userManager.FindByNameAsync(userName);
                var userQuestion = new UserQuestion
                {
                    UserId =user.Id, 
                    QuestionId = questionId, 
                    Type = 0
                };
                var add = manager.Insert(userQuestion);
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
            var find = await manager.Find(userId, questionId);
            if (find != null)
            {
                var userQuestion = new UserQuestion
                {
                    UserId = userId, 
                    QuestionId = questionId, 
                    Type = type
                };
                var updated = manager.Update(userQuestion);
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
            var deleted = await manager.Delete(userId, questionId);
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
