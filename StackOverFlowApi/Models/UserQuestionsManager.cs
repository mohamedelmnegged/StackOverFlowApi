using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using StackOverFlowApi.Data.Repo;
using StackOverFlowApi.Data.Tables;
using StackOverFlowApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Models
{
    public class UserQuestionsManager : IServicesRepo<UserQuestion>
    {
        private readonly ApplicationDBContext context;

        public UserQuestionsManager(ApplicationDBContext context)
        {
            this.context = context;
        }
        public int Insert(UserQuestion e)
        {
        
            try
            {
                if (e != null)
                {
                    var insert = this.context.UserQuestions.Add(e);
                    if (insert != null)
                    {
                        this.context.SaveChanges();
                        return 200;
                    }else
                    {
                        return 400;
                    }
                } else
                {
                    return 404;
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        
        }

        public IEnumerable<UserQuestion> GetAll()
        {
            return this.context.UserQuestions.Where(a => a.QuestionId > 0);
        }
        public async Task<UserQuestion> Find(int id)
        {
            return  this.context.UserQuestions.Where(a => a.QuestionId == id).FirstOrDefault(); 
        } 
        public async Task<UserQuestion> Find(string userId, int questionId)
        {
            return await this.context.UserQuestions.FindAsync(userId, questionId);
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var find = await Find(id);
                if (find != null)
                {
                    var delete = this.context.UserQuestions.Remove(find);
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
        public async Task<bool> Delete(string userId, int questionId)
        {
            try
            {
                var find = await Find(userId, questionId) ;
                if (find != null)
                {
                    //var delete = find.ToList().RemoveAll(r =>  true);
                    var delete = this.context.UserQuestions.Remove(find);
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

        public async Task<UserQuestion> Update(UserQuestion e)
        {
            try
            {
                var find = await Find(e.UserId, e.QuestionId);
                if (find != null)
                {
                    find.Type = e.Type;
                    this.context.SaveChanges();
                    return find; 
                }
                return null;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

        }
        public bool checkUserIsExist(string name)
        {
            return this.context.Users.Any(a => a.UserName == name);
        }
         
    }
}
