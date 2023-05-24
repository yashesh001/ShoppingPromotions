﻿namespace app.shoppingpromotions.infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
