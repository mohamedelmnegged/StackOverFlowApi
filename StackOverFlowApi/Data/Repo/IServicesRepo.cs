using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverFlowApi.Data.Repo
{
    public interface IServicesRepo<Entity>
    {
        Task< Entity> Find(int id);
        IEnumerable<Entity> GetAll();
        Task<Entity> Update(Entity e);
        Task<bool> Delete(int id);
        int Insert(Entity e);           
    }
}
