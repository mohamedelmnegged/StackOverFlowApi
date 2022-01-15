using Microsoft.EntityFrameworkCore;
using StackOverFlowApi.Data;
using StackOverFlowApi.Data.Repo;
using StackOverFlowApi.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Models
{
    public class QuestionTagManager : IServicesRepo<QuestionTag>
    {
        private readonly ApplicationDBContext context;

        public QuestionTagManager(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> Delete(int id)
        {
            throw new Exception(); 
            //var find = await find(id);
        }
        public async Task<bool> Delete(int questionId, int tagId)
        {
            try
            {
                var find = await Find(questionId, tagId);
                if (find != null)
                {
                    var remove = this.context.questionsTags.Remove(find);
                    this.context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<QuestionTag> Find(int id)
        {
            throw new Exception();
            //return await this.context.questionsTags.FindAsync(id);
        }
        public async Task<QuestionTag> Find(int questionId, int tagId)
        {
            if (questionId != null && tagId != null)
            {
                return await this.context.questionsTags.FindAsync(tagId, questionId);
            }
            return null; 
        }

        public IEnumerable<QuestionTag> GetAll()
        {
            return this.context.questionsTags.Where(a => a.QuestionId > 0); 
        }

        public  int Insert(QuestionTag e)
        {
            try
            {
                if (e != null)
                {
                    var checkQuestion = this.context.Questions.Any(a => a.Id == e.QuestionId);
                    var checkTag = this.context.Tags.Any(a => a.Id == e.TagId);
                    var checkQuestionTag = this.context.questionsTags.Any(a => a.TagId == e.TagId && a.QuestionId == e.QuestionId);
                    if (checkQuestion && checkTag )
                    {
                        if (checkQuestionTag == false)
                        {
                            var insert = this.context.questionsTags.Add(e);
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
                            return 500; 
                        }
                    }
                    else
                    {
                        return 404; 
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

        public async Task<QuestionTag> Update(QuestionTag e)
        {
            throw new Exception("Can't make an update to question tag table");
        } 

    }
}
