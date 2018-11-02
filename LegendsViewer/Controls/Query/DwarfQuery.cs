﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace LegendsViewer.Controls.Query
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            return f => true;
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return f => false;
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }

    public enum QueryOperator
    {
        And = 0,
        Or = 1
    }

    public enum QueryComparer
    {
        Equals,
        StringEquals,
        NotEqual,
        StringNotEqual,
        StartsWith,
        NotStartsWith,
        EndsWith,
        NotEndsWith,
        Contains,
        NotContains,
        GreaterThan,
        LessThan,
        Count,
        Min,
        Max,
        Sum,
        Average,
        All,
        Is,
        IsNot,
        ListEquals,
        ListGreaterThan,
        ListLessThan,
        Property
    }
}