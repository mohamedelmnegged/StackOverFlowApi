using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class QuestionTagApi : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public QuestionTagApi( UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IEnumerable<QuestionTag> getAll()
        {
            return this.unitOfWork.QuestionTagManager.GetAll(); 
        }
        [HttpGet("GetByQuestionIdAndTagId")]
        public async Task<QuestionTag> GetByQuestionIdAndTagId(int tagId, int questionId)
        {
            return await this.unitOfWork.QuestionTagManager.Find(questionId, tagId);
        }
        [HttpPost]
        public async Task<string> Add(int questionId, int tagId)
        {
            var questionTag = new QuestionTag { QuestionId = questionId, TagId = tagId };
            var insert = this.unitOfWork.QuestionTagManager.Insert(questionTag);
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
            var check = await this.unitOfWork.QuestionTagManager.Find(questionId, tagId); 
            if(check != null)
            {
                var delete = await this.unitOfWork.QuestionTagManager.Delete(questionId, tagId);
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
