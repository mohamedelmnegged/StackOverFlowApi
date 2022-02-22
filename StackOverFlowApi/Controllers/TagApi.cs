using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverFlowApi.Data.Tables;
using StackOverFlowApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StackOverFlowApi.Controllers.Apis
{

    [ApiController]
    [Route("tags")]
    [Authorize]
    public class TagApi : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly UnitOfWork unitOfWork;

        public TagApi( UserManager<User> userManager, UnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }  
        [HttpGet]
        public IEnumerable<Tag> getAll()
        {
            return this.unitOfWork.TagManager.GetAll();
            //return this.tagManager.GetAll();
        } 
        [HttpGet("GetByUserId")]
        public IEnumerable<Tag> GetByUserId(string userId)
        {
            return this.unitOfWork.TagManager.getByUserId(userId);
        } 
        [HttpGet("GetOrderedByViews")]
        public IEnumerable<Tag> GetOrderedByViews()
        {
            return this.unitOfWork.TagManager.OrderByViews();
        }
        [HttpPost]
        public async Task<string> add(string userName, string tagName, string desc)
        {
            var check = this.unitOfWork.TagManager.checkUserIsExist(userName);
            if (check)
            {
                var user = await userManager.FindByNameAsync(userName);
                var tag = new Tag {
                    Name = tagName, 
                    Description = desc,
                    Views = 0,
                    UserId = user.Id
                };
                var add = this.unitOfWork.TagManager.Insert(tag);
                if (add == 200)
                {
                    return "Success";
                }
            }
            return "Faild";
        }
        [HttpPut]
        public async Task<Tag> update(int id, string name, string desc)
        {
            var find = await this.unitOfWork.TagManager.Find(id);
            if (find != null)
            {
                var tag = new Tag
                {
                   Id = id, 
                   Description = desc, 
                   Name = name
                };
                var updated = this.unitOfWork.TagManager.Update(tag);
                if (updated != null)
                {
                    return await updated;
                }
            }
            return find;
        }

        [HttpDelete]
        public async Task<string> delete(int id)
        {
            var deleted = await this.unitOfWork.TagManager.Delete(id);
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
