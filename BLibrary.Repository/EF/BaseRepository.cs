using BLibrary.Entity.Paging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLibrary.Repository.EF
{
    public class BaseRepository<T> where T : class
    {
        DbContext context = null;//todo

        public virtual T Find(params object[] keyValues)
        {
            return context.Set<T>().Find(keyValues);
        }

        public virtual List<T> Find(Expression<Func<T, bool>> predicate, bool noTracking = false, params string[] includes)
        {
            DbQuery<T> dbset = context.Set<T>();
            foreach (string include in includes)
            {
                dbset = dbset.Include(include);
            }
            if (noTracking)
            {
                return dbset.Where(predicate).AsNoTracking().ToList();
            }
            else
            {
                return dbset.Where(predicate).ToList();
            }

        }

        public virtual T Add(T entity)
        {
            context.Set<T>().Add(entity);
            return context.SaveChanges() > 0 ? entity : null;
        }

        public virtual T Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges() > 0 ? entity : null;
        }

        public virtual bool Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            return context.SaveChanges() > 0 == true;
        }

        #region Paging
        public virtual PagedListResult<T> Search(SearchQuery<T> searchQuery)
        {
            IQueryable<T> sequence = context.Set<T>();

            sequence = ManageFilters(searchQuery, sequence);

            sequence = ManageIncludeProperties(searchQuery, sequence);

            sequence = ManageSortCriterias(searchQuery, sequence);

            return GetTheResult(searchQuery, sequence);
        }
        protected virtual IQueryable<T> ManageFilters(SearchQuery<T> searchQuery, IQueryable<T> sequence)
        {
            if (searchQuery.Filters != null && searchQuery.Filters.Count > 0)
            {
                foreach (var filterClause in searchQuery.Filters)
                {
                    sequence = sequence.Where(filterClause);
                }
            }
            return sequence;
        }
        protected virtual IQueryable<T> ManageIncludeProperties(SearchQuery<T> searchQuery, IQueryable<T> sequence)
        {
            if (!string.IsNullOrWhiteSpace(searchQuery.IncludeProperties))
            {
                var properties = searchQuery.IncludeProperties.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var includeProperty in properties)
                {
                    sequence = sequence.Include(includeProperty);
                }
            }
            return sequence;
        }
        protected virtual IQueryable<T> ManageSortCriterias(SearchQuery<T> searchQuery, IQueryable<T> sequence)
        {
            if (searchQuery.SortCriterias != null && searchQuery.SortCriterias.Count > 0)
            {
                var sortCriteria = searchQuery.SortCriterias[0];
                var orderedSequence = sortCriteria.ApplyOrdering(sequence, false);

                if (searchQuery.SortCriterias.Count > 1)
                {
                    for (var i = 1; i < searchQuery.SortCriterias.Count; i++)
                    {
                        var sc = searchQuery.SortCriterias[i];
                        orderedSequence = sc.ApplyOrdering(orderedSequence, true);
                    }
                }
                sequence = orderedSequence;
            }
            else
            {
                sequence = ((IOrderedQueryable<T>)sequence).OrderBy(x => (true));
            }
            return sequence;
        }
        protected virtual PagedListResult<T> GetTheResult(SearchQuery<T> searchQuery, IQueryable<T> sequence)
        {
            //Counting the total number of object.
            var resultCount = sequence.Count();

            var result = (searchQuery.Take > 0)
                                ? (sequence.Skip(searchQuery.Skip).Take(searchQuery.Take).ToList())
                                : (sequence.ToList());

            // Setting up the return object.
            bool hasNext = (searchQuery.Skip <= 0 && searchQuery.Take <= 0) ? false : (searchQuery.Skip + searchQuery.Take < resultCount);
            return new PagedListResult<T>()
            {
                Records = result,
                HasNext = hasNext,
                HasPrevious = (searchQuery.Skip > 0),
                Count = resultCount,
                PageSize = searchQuery.Take
            };
        }
        #endregion
    }
}
