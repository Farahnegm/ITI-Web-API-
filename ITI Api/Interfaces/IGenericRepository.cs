using ITI_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Api.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T? GetById(int id);
        T Add(T entity);
        void Update(T entity,int id);
        T? Delete( int id);
        void Save();
    }

}
