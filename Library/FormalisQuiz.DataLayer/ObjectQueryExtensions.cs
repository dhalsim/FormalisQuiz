using System;
using System.Data.Objects;
using System.Linq.Expressions;

namespace ForlamisQuiz.UI.Extensions
{
    public static class ObjectQueryExtensions
    {
        public static ObjectQuery<T> Include<T>(this ObjectQuery<T> query, Expression<Func<T, object>> selector)
        {
            string propertyName = GetPropertyName(selector);
            return query.Include(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T, object>> expression)
        {
            MemberExpression memberExpr = expression.Body as MemberExpression;
            if (memberExpr == null)
                throw new ArgumentException("Expression body must be a member expression");
            return memberExpr.Member.Name;
        }
    }
}