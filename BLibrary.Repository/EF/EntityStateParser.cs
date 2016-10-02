using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLibraty.Repository.EF
{
    /// <summary>
    ///  EF for one to many table relation, use this class to parse entity state
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityStateParser<T>
        where T : class
    {
        #region 屬性

        public IList<T> NewSource { get; set; }
        public IList<T> ExistSource { get; set; }
        public IList<string> PrimaryKeys { get; set; }
        public DbContext Context { get; set; }
        public Expression<Func<T, bool>> LoadExistDataPredicate { get; set; }
        #endregion

        #region public method
        /// <summary>
        /// constuct function
        /// </summary>
        /// <param name="context">Entity Framework context</param>
        /// <param name="source">added,deleted,edited source</param>
        /// <param name="predicate">a filter used to search existed source</param>
        public EntityStateParser(DbContext context, IList<T> source, Expression<Func<T, bool>> predicate)
        {
            Context = context;
            LoadExistDataPredicate = predicate;
            PrimaryKeys = GetPrimaryKeys(context);
            NewSource = source;
            ExistSource = Context.Set<T>().AsQueryable().Where(LoadExistDataPredicate).AsNoTracking().ToList();
        }

        /// <summary>
        /// parse entity state for each item
        /// </summary>
        /// <param name="context"></param>
        public void Parse()
        {
            var addedItems = NewSource.Where(n => !ExistSource.Any(o => CompareObject(o, n))).ToList();

            var modifiedItems = NewSource.Where(n => ExistSource.Any(o => CompareObject(o, n))).ToList();

            var deletedItems = ExistSource.Where(o => !NewSource.Any(n => CompareObject(o, n))).ToList();

            foreach (var item in addedItems)
            {
                Context.Entry(item).State = EntityState.Added;
            }

            foreach (var item in deletedItems)
            {
                Context.Entry(item).State = EntityState.Deleted;
            }

            foreach (var item in modifiedItems)
            {
                Context.Entry(item).State = EntityState.Modified;
            }
        }

        #endregion

        #region internal method

        /// <summary>
        /// get primary for entity
        /// </summary>
        /// <param name="context"></param>
        private IList<string> GetPrimaryKeys(DbContext context)
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            var set = objectContext.CreateObjectSet<T>();
            return set.EntitySet.ElementType.KeyMembers.Select(f => f.Name).ToList();
        }

        /// <summary>
        /// get property value by property name
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private int GetPropertyValue(object obj, string key)
        {
            var propertyValue = obj.GetType().GetProperty(key).GetValue(obj);

            if (propertyValue is string)
            {
                propertyValue = propertyValue.ToString().Trim();
            }
            return propertyValue.GetHashCode();
        }

        /// <summary>
        /// compare if two entity is the same one
        /// </summary>
        /// <param name="src"></param>
        /// <param name="tg"></param>
        /// <returns></returns>
        private bool CompareObject(T src, T tg)
        {
            bool isEqual = true;
            foreach (var key in PrimaryKeys)
            {
                if (!GetPropertyValue(src, key).Equals(GetPropertyValue(tg, key)))
                {
                    isEqual = false;
                    break;
                }
            }
            return isEqual;
        }

        #endregion

    }


}
