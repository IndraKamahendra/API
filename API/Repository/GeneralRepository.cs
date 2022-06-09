using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }
        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public Entity Get(Key key)
        {
            return entities.Find(key);
        }

        public int Insert(Entity entity)
        {        
            try
            {
                myContext.Add(entity);
                var result = myContext.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int Update(Entity entity)
        {
            try
            {
                myContext.Entry(entity).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int Delete(Key key)
        {
            var entity = entities.Find(key);
            if (entity == null)
            {
                return 0;
            }
            else
            {
                myContext.Remove(entity);
                var result = myContext.SaveChanges();
                return result;
            }
        }
    }
}
