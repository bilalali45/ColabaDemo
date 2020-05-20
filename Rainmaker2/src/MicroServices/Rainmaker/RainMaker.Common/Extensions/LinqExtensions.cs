using System;
using System.Collections.Generic;
//using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
//using System.Reflection;

namespace RainMaker.Common.Extensions
{

    public static class PredicateBuilder
    {
        //private static readonly MethodInfo asNonUnicodeMethodInfo =
        //            typeof(EntityFunctions).GetMethod("AsNonUnicode");
        //private static readonly MethodInfo stringEqualityMethodInfo =
        //            typeof(string).GetMethod("op_Equality");

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
                    //Expression.Call(null,
                    //    asNonUnicodeMethodInfo,
                        Expression.Constant(value, typeof(int))
                        //,
                    //false,
                    //stringEqualityMethodInfo);
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
