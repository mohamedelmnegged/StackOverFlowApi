using Microsoft.AspNetCore.Mvc;
using StackOverFlowApi.Data.Tables;
using StackOverFlowApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Controllers
{ 
    [ApiController]
    [Route("QuestionTags")]
    public class QuestionTagApi : Controller
    {
        private readonly QuestionTagManager questtionTagManager;

        public QuestionTagApi(QuestionTagManager questtionTagManager)
        {
            this.questtionTagManager = questtionTagManager;
        }
        [HttpGet]
        public IEnumerable<QuestionTag> getAll()
        {
            return this.questtionTagManager.GetAll(); 
        }
        [HttpGet("GetByQuestionIdAndTagId")]
        public async Task<QuestionTag> GetByQuestionIdAndTagId(int tagId, int questionId)
        {
            return await this.questtionTagManager.Find(questionId, tagId);
        }
        [HttpPost]
        public async Task<string> Add(int questionId, int tagId)
        {
            var questionTag = new QuestionTag { QuestionId = questionId, TagId = tagId };
            var insert = this.questtionTagManager.Insert(questionTag);
            if (insert == 200)
            {
                return "Success";
            }
            else
            {
                return "Faild";
            }
            
        }
        [HttpDelete]
        public async Task<string> Delete(int tagId, int questionId)
        {
            var check = await this.questtionTagManager.Find(questionId, tagId); 
            if(check != null)
            {
                var delete = await this.questtionTagManager.Delete(questionId, tagId);
                if (delete)
                {
                    return "Success";
                }else
                {
                    return "Faild";
                }
            }
            else
            {
                return "Not Exsit";
            }
        }
    }
}
