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
    public class FollowerManager : IServicesRepo<Followers>
    {
        private readonly ApplicationDBContext context;

        public FollowerManager(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(string userId, string followerId)
        {
            try
            {
                var find = await Find(userId, followerId);
                if (find != null)
                {
                    var delete = this.context.Followers.Remove(find);
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

        public async Task<Followers> Find(int id)
        {
            // because the id is string and the function paramter is int and in the provider interface
            //throw new Exception(); 
            return await this.context.Followers.FindAsync(id);
        } 
        public async Task<Followers>Find(string userId, string followerId)
        {
            return await context.Followers.FindAsync(userId, followerId);
        }

        public IEnumerable< Followers> FindFollowers(string userId)
        {
            var followers = context.Followers.Where(a => a.UserId == userId); 
            if(followers.Count() > 0)
            {
                return followers;
            }
            return null;
        }
        public IEnumerable<Followers> GetAll()
        {
            return this.context.Followers.Select(a => a);
        }

        public int Insert(Followers e)
        {
            try
            {
                if (e != null)
                {
                    
                    var insert = this.context.Followers.Add(e);
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

        public async Task<Followers> Update(Followers e)
        {
            try
            {
                var followerId = this.context.Followers.Where(a => a.UserId == e.UserId).Select(s => s.FollowingId).FirstOrDefault();
                var find = await Find(e.UserId, followerId);
                if (find != null)
                {
                   var deleted = await Delete(e.UserId, followerId);
                    if (deleted)
                    {
                        var inserted = new Followers { UserId = e.UserId, FollowingId = e.FollowingId };
                        var update =  Insert(inserted);
                        if(update == 200)
                        {
                            this.context.SaveChanges();
                            return inserted;
                        }
                    }
                }
                return null;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
        public bool checkUserIsExist(string id)
        {
            return this.context.Users.Any(a => a.Id == id);
        } 
        public IEnumerable<Following> GetFollowing(string userId)
        {
            var check = this.context.Users.Any(a => a.Id == userId);
            if (check)
            {
                return this.context.OwnUser
                    .Join(this.context.Followers.Where(a => a.FollowingId == userId),
                        user => user.Id,
                        following => following.UserId,
                        (user, following) => new Following { 
                            Name = user.UserName,
                            Email = user.Email, 
                            JobTitle = user.JobTitle,
                            UserId = user.Id,
                            ImageUrl = user.ImageUrl
                        }
                    );
            }
            return null; 
        }
    }
}
