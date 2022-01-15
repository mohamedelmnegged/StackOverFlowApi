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
    public class TagApi : ControllerBase
    {
        private readonly TagManager tagManager;
        private readonly UserManager<User> userManager;

        public TagApi(TagManager tagManager, UserManager<User> userManager)
        {
            this.tagManager = tagManager;
            this.userManager = userManager;
        }  
        [HttpGet]
        public IEnumerable<Tag> getAll()
        {
            return this.tagManager.GetAll();
        } 
        [HttpGet("GetByUserId")]
        public IEnumerable<Tag> GetByUserId(string userId)
        {
            return tagManager.getByUserId(userId);
        } 
        [HttpGet("GetOrderedByViews")]
        public IEnumerable<Tag> GetOrderedByViews()
        {
            return tagManager.OrderByViews();
        }
        [HttpPost]
        public async Task<string> add(string userName, string tagName, string desc)
        {
            var check = tagManager.checkUserIsExist(userName);
            if (check)
            {
                var user = await userManager.FindByNameAsync(userName);
                var tag = new Tag {
                    Name = tagName, 
                    Description = desc,
                    Views = 0,
                    UserId = user.Id
                };
                var add = tagManager.Insert(tag);
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
            var find = await tagManager.Find(id);
            if (find != null)
            {
                var tag = new Tag
                {
                   Id = id, 
                   Description = desc, 
                   Name = name
                };
                var updated = tagManager.Update(tag);
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
            var deleted = await tagManager.Delete(id);
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
