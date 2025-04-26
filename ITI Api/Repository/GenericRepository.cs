using ITI_Api.Interfaces;
using ITI_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ITI_Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ITIContext _db;

        public GenericRepository(ITIContext db)
        {
            _db = db;
        }

        IQueryable<T> IGenericRepository<T>.GetAll()
        {
            return _db.Set<T>();
        }

        T? IGenericRepository<T>.GetById(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public T Add(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
            return entity;
        }


        public void Update(T entity, int id)
        {
            var existingEntity = _db.Set<T>().Find(id);
            if (existingEntity == null)
            {
                 new NotFoundResult();
            }

            _db.Entry(existingEntity).CurrentValues.SetValues(entity);

            _db.SaveChanges();

        }

        public T? Delete(int id)
        {
            var existingEntity = _db.Set<T>().Find(id);
            if (existingEntity == null) return null;

            _db.Set<T>().Remove(existingEntity);
            _db.SaveChanges();
            return existingEntity;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

       
    }
}
