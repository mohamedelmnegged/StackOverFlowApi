using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverFlowApi.Data;
using StackOverFlowApi.Data.Repo;
using StackOverFlowApi.Data.Tables;
using StackOverFlowApi.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Models
{
    public class QuestionManager : IServicesRepo<Question>
    {
        private readonly ApplicationDBContext context;

        public QuestionManager(ApplicationDBContext context)
        {
            this.context = context;
        }
        public int Insert(Question e)
        {
            try
            {
                if (e != null)
                {
                    // var insert = this.context.Questions.FromSqlRaw("exec insertIntoQuestions ", e);
                    var insert = this.context.Questions.Add(e);

                    if (insert != null)
                    {
                        this.context.SaveChanges();
                        return 200;
                    }
                    else
                    {
                        return 400;
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

        public IEnumerable<Question> GetAll()
        {
            return this.context.Questions
                .Include(a => a.User)
                .Include(s => s.UserQuestions)
                .Include(a => a.questionsTags)
               // .Include(a => a.t)
                .Where(i => i.Id > 0);
        }
        public async Task<Question> Find(int id)
        {
            if (id != null)
            {
                var question = await this.context.Questions.FindAsync(id);
                if (question != null)
                {
                    return question;
                }
            }
            return null;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var find = await Find(id);
                if (find != null)
                {
                    var delete = this.context.Questions.Remove(find);
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

        public async Task<Question> Update(Question e)
        {
            try
            {
                var find = await Find(e.Id);
                if (find != null)
                {
                    find.Body = e.Body;
                    find.Title = e.Title;
                 
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

        public async Task<IEnumerable<Question>> getByUserId(string userId)
        {
            var check = this.context.Users.Any(a => a.Id == userId);
            if (check)
            {
                var questions = this.context.Questions.Where(a => a.User.Id == userId);
                return questions;
            }
            return null; 
        }
        public IEnumerable<Question> OrderBy(Func<Question, int> a, string By)
        {
            if (By == "ASC")
            {
                return this.context.Questions.OrderBy(a);
            }
            else
            {
                return this.context.Questions.OrderByDescending(a);
            }
        }public IEnumerable<Question> OrderBy(Func<Question, string> a, string By)
        {
            if (By == "ASC")
            {
                return this.context.Questions.OrderBy(a);
            }
            else
            {
                return this.context.Questions.OrderByDescending(a);
            }
        }
        public IEnumerable<QuestionTagView> GeQuestionsByTagIdOrdered(int tagId)
        {
            var check = this.context.Tags.Any(a => a.Id == tagId);
            if (check)
            {
               return this.context.Questions.OrderByDescending(a => a.Views).Join(
                    this.context.questionsTags.Where(a => a.TagId == tagId),
                    question => question.Id,
                    tag => tag.QuestionId,
                    (question, tag) => new QuestionTagView
                    {
                        QuestionId = question.Id,
                        QuestionBody = question.Body,
                        QuestionUser = question.User
                    });
            }
            return null; 
        } 

        public  IEnumerable<HomeQuestions> GetHomeQuestions()
        {
            return this.context.Questions
                 .Select(
                 s => new HomeQuestions
                 {
                     QuestionBody = s.Body, 
                     QuestionDownvoted = s.Downvoted,
                     QuestionId = s.Id,
                     QuestionTitle = s.Title,
                     QuestionUpvoted = s.Upvoted,
                     QuestionViews = s.Views,
                     tags = s.questionsTags.Join(
                         this.context.Tags,
                         questionTag => questionTag.TagId,
                         tag => tag.Id,
                         (questionTag, tag) => new TagView {
                             TagId = tag.Id,
                             TagName = tag.Name,
                         }
                     )
                 }).OrderByDescending(a => a.QuestionViews); 

        }
    }
}
