using Microsoft.AspNetCore.Mvc;
using StackOverFlowApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackOverFlowApi.Data.Tables;
using Microsoft.AspNetCore.Identity;
using StackOverFlowApi.ViewsModel;

namespace StackOverFlowApi.Controllers.Apis
{

    [ApiController]
    [Route("followers")]
    public class FollowerApi : ControllerBase
    {
        private readonly FollowerManager followerManager;
        private readonly UserManager<User> userManager;

        public FollowerApi(FollowerManager followerManager, UserManager<User> userManager)
        {
            this.followerManager = followerManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IEnumerable<Followers> getAll()
        {
            return this.followerManager.GetAll();
        } 
        [HttpGet("GetFollowersByUserId")]
        public IEnumerable<Followers> get(string id)
        {
            var find = followerManager.FindFollowers(id);
            if(find != null)
            {
                return find;
            }
            return null;
        } 
        [HttpGet("GetFollowingByUserId")]
        public IEnumerable<Following> GetFollowing(string userid)
        {
            return followerManager.GetFollowing(userid);
        }
        [HttpPost]
        public async Task<string> add(string userId, string followerId)
        {
            var checkUser = followerManager.checkUserIsExist(userId);
            var checkFollower = followerManager.checkUserIsExist(followerId);
            if (checkUser && checkFollower)
            {
               
                var follow = new Followers
                {
                    UserId = userId,
                    FollowingId = followerId,
                };
                var add = this.followerManager.Insert(follow);
                if (add == 200)
                {
                    return "Successed";
                }
            }
            return "Faild";
        }
        [HttpPut]
        public async Task<Followers> update(string userId, string followerId) 
        {
            var findUser =  followerManager.checkUserIsExist(userId);
            var findFollower =  followerManager.checkUserIsExist(followerId);
            if (findUser && findFollower)
            {
                var follow = new Followers { UserId = userId, FollowingId = followerId };
                var updated = followerManager.Update(follow); 
                if(updated != null)
                {
                    return await updated; 
                }
            }
            return new Followers { UserId = "", FollowingId = "" };
        } 

        [HttpDelete]
        public async Task<string> delete(string userId, string followerId)
        {
            var findUser = followerManager.checkUserIsExist(userId);
            var findFollower = followerManager.checkUserIsExist(followerId);
            if (findUser && findFollower)
            {
                var deleted = await followerManager.Delete(userId, followerId);
                if (deleted)
                {
                    return "Succssed";
                }
            }
            return "Faild";
        }
    }
}
