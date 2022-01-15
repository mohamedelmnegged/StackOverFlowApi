using Microsoft.AspNetCore.Mvc;
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
    public class TagManager : IServicesRepo<Tag>
    {
        private readonly ApplicationDBContext context;

        public TagManager(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var find = await Find(id);
                if (find != null)
                {
                    var delete = this.context.Tags.Remove(find);
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

        public async Task<Tag> Find(int id)
        {
            return await this.context.Tags.FindAsync(id);
        }

        public IEnumerable<Tag> GetAll()
        {
            return this.context.Tags.Where(a => a.Id > 0);
        }

        public int Insert(Tag e)
        {
            
            try
            {
                if (e != null)
                {
                    var insert = this.context.Tags.Add(e);
                    if (insert != null)
                    {
                        this.context.SaveChanges(); 
                        return 200;
                    } else
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

        public async Task<Tag> Update(Tag e)
        {
            try
            {
                var find = await Find(e.Id);
                if (find != null)
                {
                    find.Name = e.Name;
                    find.Description = e.Description;

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

        public IEnumerable<Tag> getByUserId(string userId)
        {
            var check =  this.context.Users.Any(a => a.Id == userId);
            if (check)
            {
                return this.context.Tags.Where(a => a.UserId == userId);
            }
            return null; 
        } 
        public IEnumerable<Tag> OrderByViews()
        {
            return this.context.Tags.OrderByDescending(a => a.Views);
        }
    }
}
