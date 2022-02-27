using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverFlowApi.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Controllers.Apis
{
    [ApiController]
    [Route("users")]
    [Authorize(AuthenticationSchemes ="Bearer")]
    public class UsersApi : ControllerBase
    {
        private readonly UserManager<User> usermanager;

        public UsersApi(UserManager<User> usermanager)
        {
            this.usermanager = usermanager;
        }
        [HttpGet]
        public IEnumerable<User> getUsers()
        {
            return this.usermanager.Users.AsEnumerable();
        }
        [HttpPost]
        public async Task<string> addUsers(string userName, string email, int phone, string password )
        {
            var check = await usermanager.FindByNameAsync(userName);
            if (check == null)
            {
                if (userName.Trim() != "" && password.Trim() != "")
                {
                    var user = new User
                    {
                        UserName = userName,
                        Email = email,
                        PhoneNumber = phone.ToString()
                    };
                    var add = await usermanager.CreateAsync(user, password);
                    if (add.Succeeded)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Failed";
                    }
                }
            }
            return "User Exist";
        }
        
        [HttpPut]
        public async Task<User> update(string Id, string userName, string email, int phone)
        {
            var user = await usermanager.FindByIdAsync(Id);
            if (user != null)
            {
                user.UserName = userName;
                user.Email = email;
                user.PhoneNumber = phone.ToString() ;
                var updated = await usermanager.UpdateAsync(user);
                if (updated.Succeeded )
                {
                    return  user;
                }
            }
            return user;
        }

        [HttpDelete]
        public async Task<string> delete(string id)
        {
            var user = await usermanager.FindByIdAsync(id);
            if (user != null)
            {
                var deleted = await usermanager.DeleteAsync(user);
                if (deleted.Succeeded)
                {
                    return "Successed";
                }
                
            }
            return "Faild";
        }


    }
}
