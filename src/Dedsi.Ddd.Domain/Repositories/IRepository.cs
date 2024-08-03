using System.Linq.Expressions;

namespace Dedsi.Ddd.Domain.Repositories;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> InsertAsync(List<TEntity> entities);
    
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> InsertAsync(TEntity entity);
    
    /// <summary>
    /// 新增后返回自增列
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<int> InsertReturnIdentityAsync(TEntity entity);
    
    /// <summary>
    /// 所有字段更新
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(TEntity entity);
    
    /// <summary>
    /// 只更新某列
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    Task<bool> UpdateColumnsAsync(TEntity entity, Expression<Func<TEntity, object>> columns);

    /// <summary>
    /// 不更新某列
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    Task<bool> IgnoreColumnsAsync(TEntity entity, Expression<Func<TEntity, object>> columns);

    
    /// <summary>
    /// 批量更新
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(List<TEntity> entities);
    
    /// <summary>
    /// 批量更新：只更新某列
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    Task<bool> UpdateColumnsAsync(List<TEntity> entities, Expression<Func<TEntity, object>> columns);

    /// <summary>
    /// 批量更新：不更新某列
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    Task<bool> IgnoreColumnsAsync(List<TEntity> entities, Expression<Func<TEntity, object>> columns);
    
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(TEntity entity);
    
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// 主键删除
    /// </summary>
    /// <param name="id"></param>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <returns></returns>
    Task<bool> DeleteAsync<TPrimaryKey>(TPrimaryKey id);
    
    /// <summary>
    /// 多个删除
    /// </summary>
    /// <param name="ids">一组主键Id</param>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <returns></returns>
    Task<bool> DeleteAsync<TPrimaryKey>(IEnumerable<TPrimaryKey> ids);

    /// <summary>
    /// 执行sql
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<int> ExecuteCommandAsync(string sql, object parameters);
}