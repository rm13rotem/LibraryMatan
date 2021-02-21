using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMatan.Models
{
    public interface IRepository<T> : IDisposable
        where T:class 
    {
        IEnumerable<T> GetAll();
        T GetByID(int id);
        bool TryInsert(T entity); // each CRUD action also saves
        bool TryDelete(T entity);
        bool TryUpdate(T entity);
    }

    public class InMemoryRepository<T> : IRepository<T>
        where T:class
    {
        private LibraryMatanContext db;
        private static List<T> _mylist = null;
        public static DateTime _lastCached;

        public InMemoryRepository(LibraryMatanContext _db)
        {
            db = _db;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void RefreshIfStale(bool forceRefreshing = false)
        {
            if (_mylist == null || _lastCached < DateTime.UtcNow.AddDays(-1) ||
                forceRefreshing == true)
            {
               var newList = db.Set<T>().ToList();
                _mylist = newList;
                _lastCached = DateTime.UtcNow;
            }
        }

        public IEnumerable<T> GetAll()
        {
            RefreshIfStale();
            return _mylist;
        }


        public T GetByID(int id)
        {
            RefreshIfStale();
            return _mylist.FirstOrDefault(x => ((dynamic)x).Id == id);
        }

        public bool TryDelete(T entity)
        {
            return true;
        }

        public bool TryInsert(T entity)
        {
            RefreshIfStale();
            try
            {
                db.Set<T>().Add(entity);
                db.SaveChanges();
                _mylist.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryUpdate(T entity)
        {
            RefreshIfStale();
            try
            {
                db.Set<T>().Attach(entity);
                db.SaveChanges();
                var toRemove = _mylist.FirstOrDefault(x => ((dynamic)x).Id == ((dynamic)entity).Id);
                if (toRemove == null) return false;
                _mylist.Remove(toRemove);
                _mylist.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
