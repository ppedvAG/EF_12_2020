using ppedv.GiftManager.Model;
using ppedv.GiftManager.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.GiftManager.Data.EF
{
    public class EfRepository : IRepository
    {
        EfContext context = new EfContext();

        public void Add<T>(T entity) where T : Entity
        {
            //if (typeof(T) == typeof(Geschenk))
            //    context.Geschenke.Add(entity as Geschenk);
            context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return context.Set<T>().ToList();
        }

        public T GetbyId<T>(int id) where T : Entity
        {
            return context.Set<T>().Find(id);
       }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return context.Set<T>();
        }

        public int SaveAll()
        {
            return context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            var loaded = GetbyId<T>(entity.Id);
            if (loaded != null)
                context.Entry(loaded).CurrentValues.SetValues(entity);
        }
    }
}
