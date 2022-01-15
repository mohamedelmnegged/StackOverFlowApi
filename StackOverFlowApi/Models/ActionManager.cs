using StackOverFlowApi.Data;
using StackOverFlowApi.Data.Repo;
using StackOverFlowApi.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Models
{
    public class ActionManager : IServicesRepo<Actions>
    {
        private readonly ApplicationDBContext context;

        public ActionManager(ApplicationDBContext context)
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
                    var delete = this.context.Actions.Remove(find);
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

        public async Task< Actions> Find(int id)
        {
            return await this.context.Actions.FindAsync(id);
        }

        public IEnumerable<Actions> GetAll()
        {
            return this.context.Actions.Where(a => a.Id > 0);
        }

        public int Insert(Actions e)
        {

            try
            {
                if (e != null)
                {
                    
                    var insert = this.context.Actions.Add(e);
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

        public async Task<Actions> Update(Actions e)
        {
            try
            {
                var find = await Find(e.Id);
                if (find != null)
                {
                    find.Name = e.Name;
                    find.Description = e.Description;
                    find.Status = e.Status;
                    find.Date = e.Date; 

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
    }
}
