using Microsoft.AspNetCore.Mvc;
using StackOverFlowApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using StackOverFlowApi.Data.Tables;
using Microsoft.AspNetCore.Identity;
using StackOverFlowApi.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace StackOverFlowApi.Controllers.Apis
{
    [ApiController]
    [Route("actions")]
    public class ActionApi : ControllerBase
    {
        private readonly ActionManager actionManager;
        private readonly UserManager<User> userManager;
        
        public ActionApi(ActionManager actionManager, UserManager<User> userManager)
        {
            this.actionManager = actionManager;
            this.userManager = userManager;
        }
        [HttpGet("GetAll")]
        public IEnumerable<Actions> GetAll()
        {
            return this.actionManager.GetAll();
        }
        [HttpGet("GetById")]
        public async Task<Actions> GetDone(int id)
        {
            var find = await actionManager.Find(id);
            if(find != null)
            {
                return find; 
            }
            return new Actions { Name = "" };
        }
      
        [HttpPost]
        public async Task<string> add(string userName, string actionName, string desc, string url)
        {
            var check = actionManager.checkUserIsExist(userName);
            User user; 
            if (check)
            {
               user = await userManager.FindByNameAsync(userName);
            }
            else
            {
                user = await userManager.FindByNameAsync("mohamed");
             }

            var action = new Actions
            {
                Name = actionName,
                Description = desc,
                ActionLink = url,
                Status = Enums.status.valid,
                UserId = user.Id
            };
            var add = this.actionManager.Insert(action);
            if (add == 200)
            {
                return "Successed";
            }
            else
            {
                return "Faild";
            }

        }
        
        [HttpPut]
        public async Task<Actions> update(int id, string desc, int status, string name)
        {
            var find = await actionManager.Find(id);
            if(find != null)
            {
                var action = new Actions
                {
                    Id = id,
                    Date = DateTime.Now,
                    Description = desc,
                    Status = (int)Enums.status.pending == status ? Enums.status.pending : Enums.status.valid,
                    Name = name
                };
                var updated =  actionManager.Update(action);
                if(updated != null)
                {
                    return await updated;
                }
            }
            return find;
        }

        [HttpDelete]
        public async Task<string> delete(int id) {
            var deleted = await actionManager.Delete(id);
            if (deleted)
            {
                return "Successed";
            } else
            {
                return "Faild";
            }
        }


    }
}
