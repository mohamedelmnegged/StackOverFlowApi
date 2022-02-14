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
        private readonly UnitOfWork unitOfWork;

        public FollowerApi( UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IEnumerable<Followers> getAll()
        {
            return this.unitOfWork.FollowerManager.GetAll();
        } 
        [HttpGet("GetFollowersByUserId")]
        public IEnumerable<Followers> get(string id)
        {
            var find = this.unitOfWork.FollowerManager.FindFollowers(id);
            if(find != null)
            {
                return find;
            }
            return null;
        } 
        [HttpGet("GetFollowingByUserId")]
        public IEnumerable<Following> GetFollowing(string userid)
        {
            return this.unitOfWork.FollowerManager.GetFollowing(userid);
        }
        [HttpPost]
        public async Task<string> add(string userId, string followerId)
        {
            var checkUser = this.unitOfWork.FollowerManager.checkUserIsExist(userId);
            var checkFollower = this.unitOfWork.FollowerManager.checkUserIsExist(followerId);
            if (checkUser && checkFollower)
            {
               
                var follow = new Followers
                {
                    UserId = userId,
                    FollowingId = followerId,
                };
                var add = this.unitOfWork.FollowerManager.Insert(follow);
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
            var findUser = this.unitOfWork.FollowerManager.checkUserIsExist(userId);
            var findFollower = this.unitOfWork.FollowerManager.checkUserIsExist(followerId);
            if (findUser && findFollower)
            {
                var follow = new Followers { UserId = userId, FollowingId = followerId };
                var updated = this.unitOfWork.FollowerManager.Update(follow); 
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
            var findUser = this.unitOfWork.FollowerManager.checkUserIsExist(userId);
            var findFollower = this.unitOfWork.FollowerManager.checkUserIsExist(followerId);
            if (findUser && findFollower)
            {
                var deleted = await this.unitOfWork.FollowerManager.Delete(userId, followerId);
                if (deleted)
                {
                    return "Succssed";
                }
            }
            return "Faild";
        }
    }
}
