using System.Linq.Expressions;

namespace Dedsi.Core.Extensions;

/// <summary>
/// Expression 扩展
/// </summary>
public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> GetTrue<T>() { return f => true; }
    
    public static Expression<Func<T, bool>> GetFalse<T>() { return f => false; }

    /// <summary>
    /// 扩展 WhereIF
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="condition"></param>
    /// <param name="expression2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Expression<Func<T, bool>> WhereIf<T>(this Expression<Func<T, bool>> expression,bool condition, Expression<Func<T, bool>> expression2)
    {
        if (condition)
        {
            expression = expression.And(expression2);
        }
        return expression;
    }
}
