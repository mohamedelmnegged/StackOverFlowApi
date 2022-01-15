using StackOverFlowApi.Data;
using StackOverFlowApi.Data.Repo;
using StackOverFlowApi.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Models
{
    public class WebsiteManager : IServicesRepo<Websites>
    {
        private readonly ApplicationDBContext context;

        public WebsiteManager(ApplicationDBContext context)
        {
            this.context = context;
        }
        
        public async Task<bool> Delete(int id)
        {
            try
            {
                var delete = await Find(id);
                if (delete != null)
                {
                    this.context.Websites.Remove(delete);
                    this.context.SaveChanges();
                    return true;
                }
                return false;
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Websites> Find(int id)
        {
            if (id != null)
            {
                var find = context.Websites.FindAsync(id);
                if (find != null)
                    return await find;
            }
            return null; 
        }

        public IEnumerable<Websites> GetAll()
        {
            return context.Websites.Where(i => i.Id > 0);  
        }

        public int Insert(Websites e)
        {
            
            try
            {
                if (e != null)
                {
                    var insert = context.Websites.Add(e);
                    if (insert != null)
                    {
                        this.context.SaveChanges();
                        return 200;
                    }else
                    {
                        return 400;
                    }
                }else
                {
                    return 404;
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
                
 

        }

        public async Task<Websites> Update(Websites e)
        {
            try
            {
                if (e != null)
                {
                    var find = await Find(e.Id);
                    if (find != null)
                    {
                        find.Name = e.Name;
                        find.WebsiteIcon = e.WebsiteIcon;
                        find.WebsiteLink = e.WebsiteLink;
                        this.context.SaveChanges();
                        return find;
                    }
                }
                return null; 
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
