using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Extensions.ExtensionClasses
{

    public static class PredicateBuilder
    {
        public static Expression<Func<TEntity, bool>> In<TEntity>(
                    IEnumerable<int> source,
                    Expression<Func<TEntity, int>> expression)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (expression == null) throw new ArgumentNullException("expression");

            Expression predicate = null;
            foreach (int value in source)
            {
                var fragment = Expression.Equal(
                    expression.Body,
                        Expression.Constant(value, typeof(int))
                    );
                predicate = predicate == null ? fragment : Expression.OrElse(predicate, fragment);
            }

            if (predicate != null)
                return Expression.Lambda<Func<TEntity, bool>>(predicate,
                    ((LambdaExpression)expression).Parameters);
            return Expression.Lambda<Func<TEntity, bool>>(Expression.NotEqual(Expression.Constant(1, typeof(int)), Expression.Constant(1, typeof(int))), ((LambdaExpression)expression).Parameters);
        }
    }

    public static class DistinctRecordsBy
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var known = new HashSet<TKey>();
            return source.Where(element => known.Add(keySelector(element)));
        }
    }

}
