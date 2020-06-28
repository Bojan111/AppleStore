using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Apple.Data.Interface
{
	public interface ISql<T> where T : class
	{
		T Get(int Id);
		IEnumerable<T> GetAll(
			Expression<Func<T, bool>> filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			string includeProperties = null
			);

		T GetFirstOrDefault(
			Expression<Func<T, bool>> filter = null,
			string includeProperties = null
			);

		void Add(T entity);
		void Remove(T entity);
		void Remove(int Id);
		void RemoveRange(IEnumerable<T> entity);
	}
}
