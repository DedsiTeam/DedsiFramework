using System.Linq.Expressions;
using Dedsi.Core.DependencyInjections;
using SqlSugar;

namespace Dedsi.Ddd.Domain.Repositories;

public interface IReadOnlyRepository<TEntity> : ITransientDependency where TEntity : class
{
    /// <summary>
    /// 查询单个
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> whereExpression);
    
    /// <summary>
    /// sql查询
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="whereObj"></param>
    /// <returns></returns>
    Task<TEntity> GetAsync(string sql, object whereObj);
    
    /// <summary>
    /// sql查询
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="whereObj"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    Task<TResult> GetAsync<TResult>(string sql, object whereObj);
    
    /// <summary>
    /// 主键Id查询
    /// </summary>
    /// <param name="id"></param>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <returns></returns>
    Task<TEntity> GetAsync<TPrimaryKey>(TPrimaryKey id);
    
    /// <summary>
    /// 条件查询
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression);
    
    /// <summary>
    /// sql查询
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="whereObj"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetListAsync(string sql, object whereObj);
    
    /// <summary>
    /// sql查询
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="whereObj"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    Task<List<TResult>> GetListAsync<TResult>(string sql, object whereObj);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="whereExpression"></param>
    /// <param name="orderExpression"></param>
    /// <param name="orderByType"></param>
    /// <returns></returns>
    Task<(int, List<TEntity>)> GetPagedListAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, bool>> whereExpression,
        Expression<Func<TEntity, object>> orderExpression,
        OrderByType orderByType = OrderByType.Desc);

    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> GetListAsync();

    /// <summary>
    /// 条件查询数量
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<int> GetCountAsync(Expression<Func<TEntity, bool>> whereExpression);

    /// <summary>
    /// 查询数量
    /// </summary>
    /// <returns></returns>
    Task<int> GetCountAsync();

    /// <summary>
    /// sql查询数量
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="whereObj"></param>
    /// <returns></returns>
    Task<int> CountAsync(string sql, object whereObj);
    
    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="whereExpression"></param>
    /// <returns></returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression);
}