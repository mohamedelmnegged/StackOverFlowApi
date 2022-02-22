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
using Microsoft.AspNetCore.Authorization;

namespace StackOverFlowApi.Controllers.Apis
{
    [ApiController]
    [Route("actions")]
    [Authorize]
    public class ActionApi : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly UnitOfWork unitOfWork;

        public ActionApi( UserManager<User> userManager, UnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("GetAll")]
        public IEnumerable<Actions> GetAll()
        {
            return this.unitOfWork.ActionManager.GetAll();
            //return this.actionManager.GetAll();
        }
        [HttpGet("GetById")]
        public async Task<Actions> GetDone(int id)
        {
            var find = await this.unitOfWork.ActionManager.Find(id);
            if(find != null)
            {
                return find; 
            }
            return new Actions { Name = "" };
        }
      
        [HttpPost]
        public async Task<string> add(string userName, string actionName, string desc, string url)
        {
            var check = this.unitOfWork.ActionManager.checkUserIsExist(userName);
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
            var add = this.unitOfWork.ActionManager.Insert(action);
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
            var find = await this.unitOfWork.ActionManager.Find(id);
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
                var updated =  this.unitOfWork.ActionManager.Update(action);
                if(updated != null)
                {
                    return await updated;
                }
            }
            return find;
        }

        [HttpDelete]
        public async Task<string> delete(int id) {
            var deleted = await this.unitOfWork.ActionManager.Delete(id);
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
