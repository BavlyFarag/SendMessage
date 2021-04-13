using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;
using System.Linq.Expressions;
using Innovs.Core;

namespace Innovs.Data
{
    public class Repository<T> where T : BaseEntity
    {
        private readonly Innovs_MobilePortalContext context;
        private IDbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(Innovs_MobilePortalContext context)
        {
            this.context = context;
        }
        

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Add(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        //public IQueryable<InForm> SearchFor(Expression<Func<Core.Data.Type, bool>> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Remove(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<T>();
                }
                return entities;
            }
        }
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                {
                    throw new ArgumentNullException("entity");
                }

                return Entities.Where(predicate);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors).Aggregate(string.Empty, (current, validationError) => current + (string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }

        }

        public virtual List<T> GetAll
        {
            get
            {
                return Entities.ToList<T>();
            }
        }
        public string ExecuteScalar(string query, object[] parameters)
        {
            var result = context.Database.SqlQuery<string>(query, parameters).ToList();

            return result[0];
        }

        public List<T> ExecuteStoredProcedure(string spName, object[] parameters)
        {
            return context.Database.SqlQuery<T>(spName, parameters).ToList<T>();
        }

    }
}
